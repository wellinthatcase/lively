#ifndef UNICODE
#define UNICODE
#endif

#include <Windows.h>

#define LIVELY_EXSTYLE NULL
#define LIVELY_NAME (constexpr wchar_t[14])L"Untitled Note"

#define LIVELY_WMSG_MIN 0
#define LIVELY_WMSG_MAX 0

#define LIVELY_STD_WIN_WIDTH 500
#define LIVELY_STD_WIN_HEIGHT 500

#define MSG_INTERVAL_DURATION 65 // ms
#define LIVELY_HBRUSH (HBRUSH)(COLOR_WINDOW + 1)

LRESULT CALLBACK WndProc(
	HWND   hwnd,
	UINT   uMsg,
	WPARAM WPrm,
	LPARAM LPrm
);

INT WINAPI wWinMain(
	HINSTANCE hInstance,
	HINSTANCE prevhInst,
	LPWSTR    pCmdLine,
	INT       nCmdShow
)
{
	WNDCLASS wc = { };

	wc.style = CS_HREDRAW | CS_VREDRAW;
	wc.hCursor = LoadCursor(hInstance, IDC_ARROW);
	wc.hInstance = hInstance;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = LIVELY_NAME;
	wc.hbrBackground = LIVELY_HBRUSH;

	RegisterClassW(&wc);

	HWND hwnd = CreateWindowEx(
		LIVELY_EXSTYLE,
		LIVELY_NAME,
		LIVELY_NAME,
		WS_OVERLAPPEDWINDOW | WS_VISIBLE,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		LIVELY_STD_WIN_WIDTH,
		LIVELY_STD_WIN_HEIGHT,
		NULL,
		NULL,
		hInstance,
		NULL
	);

	if (hwnd == NULL)
		return NULL;

	ShowWindow(hwnd, nCmdShow);

	MSG message = { };
	while (GetMessage(&message, hwnd, LIVELY_WMSG_MIN, LIVELY_WMSG_MAX))
	{
		TranslateMessage(&message);
		DispatchMessageW(&message);
		Sleep(MSG_INTERVAL_DURATION);
	}

	return message.wParam ? TRUE : FALSE;
}

LRESULT CALLBACK WndProc(
	HWND   hwnd,
	UINT   uMsg,
	WPARAM WPrm,
	LPARAM LPrm
)
{
	switch (uMsg)
	{
	case WM_CLOSE:
		if (MessageBox(hwnd, L"Are you sure you wanna quit?", L"Confirm", MB_YESNO) == IDYES) {
			DestroyWindow(hwnd);
		}
		return TRUE;

	case WM_DESTROY:
		PostQuitMessage(0);
		return TRUE;

	case WM_PAINT:
	{
		PAINTSTRUCT paint = { };
		HDC hdc = BeginPaint(hwnd, &paint);

		FillRect(hdc, &paint.rcPaint, LIVELY_HBRUSH);
		EndPaint(hwnd, &paint);
	}
	return TRUE;

	default:
		return DefWindowProc(hwnd, uMsg, WPrm, LPrm);
	}
}