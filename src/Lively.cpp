#include "Lively.h"

LRESULT CALLBACK WindowProcedure(
	HWND   hwnd,
	UINT   uMsg,
	WPARAM WPrm,
	LPARAM LPrm
);

INT WINAPI WinMain(
	_In_     HINSTANCE hInstance,
	_In_opt_ HINSTANCE prevhInst,
	_In_     LPSTR     pCmdLine,
	_In_     INT       nCmdShow
)
{
	WNDCLASS wc = { };
	
	wc.hInstance     = hInstance;
	wc.lpfnWndProc   = WindowProcedure;

	wc.hIcon         = WINDOW_ICONS;
	wc.style         = WINDOW_STYLE;
	wc.hCursor       = WINDOW_CURSR;
	wc.cbClsExtra    = EXTRA_BYTES;
	wc.cbWndExtra    = EXTRA_BYTES;
	wc.lpszClassName = WINDOW_CNAME;
	wc.hbrBackground = WINDOW_BRUSH;

	RegisterClassW(&wc);

	HWND hwnd = CreateWindowExW(
		NULL,
		WINDOW_CNAME,
		WINDOW_CNAME,
		WINDOW_FLAGS,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		PX_WIN_WIDTH,
		PX_WIN_HEIGHT,
		NULL,
		NULL,
		hInstance,
		NULL
	);

	if (hwnd == NULL)
		return NULL;

	ShowWindow(hwnd, nCmdShow);

	MSG message = { };
	while (GetMessage(&message, hwnd, WMSG_MIN, WMSG_MAX))
	{
		TranslateMessage(&message);
		DispatchMessageW(&message);
		Sleep(MSG_INTERVAL_DURATION_IN_MS);
	}

	return message.wParam ? TRUE : FALSE;
}

LRESULT CALLBACK WindowProcedure(
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

		FillRect(hdc, &paint.rcPaint, WINDOW_BRUSH);
		EndPaint(hwnd, &paint);
	}
	return TRUE;

	default:
		return DefWindowProc(hwnd, uMsg, WPrm, LPrm);
	}
}