#pragma once
#ifndef LIVELY
#define LIVELY

#ifndef UNICODE
#define UNICODE
#endif

#include <Windows.h>

INT WINAPI WinMain(
	_In_     HINSTANCE hInstance,
	_In_opt_ HINSTANCE prevhInst,
	_In_     LPSTR     pCmdLine,
	_In_     INT       nCmdShow
);

LRESULT CALLBACK WindowProcedure(
	_In_ HWND   hwnd,
	_In_ UINT   uMsg,
	_In_ WPARAM WPrm,
	_In_ LPARAM LPrm
);

#define WINDOW_CURSR (HICON)   LoadCursor(hInstance, IDC_ARROW)
#define WINDOW_ICONS (HICON)   LoadIcon(hInstance, IDI_APPLICATION)
#define WINDOW_BRUSH (HBRUSH)  (COLOR_WINDOW + 1)

constexpr auto WMSG_MAX = NULL;
constexpr auto WMSG_MIN = NULL;

constexpr auto EXTRA_BYTES = 0;
constexpr auto COOLDOWN_MS = 65;

constexpr auto WINDOW_CNAME = L"Untitled Note";
constexpr auto WINDOW_STYLE = CS_HREDRAW | CS_VREDRAW;
constexpr auto WINDOW_FLAGS = WS_OVERLAPPEDWINDOW | WS_VISIBLE;

constexpr auto PX_WIN_WIDTH  = 650;
constexpr auto PX_WIN_HEIGHT = 600;

#endif /* LIVELY */