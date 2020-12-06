#pragma once
#ifndef LIVELY
#define LIVELY

#ifndef UNICODE
#define UNICODE
#endif

#include <Windows.h>

LRESULT CALLBACK WindowProcedure(
	HWND   hwnd,
	UINT   uMsg,
	WPARAM WPrm,
	LPARAM LPrm
);

#define WINDOW_STYLE (UINT)CS_HREDRAW | CS_VREDRAW
#define WINDOW_FLAGS (DWORD)WS_OVERLAPPEDWINDOW | WS_VISIBLE
#define WINDOW_CURSR (HICON)LoadCursor(hInstance, IDC_ARROW)
#define WINDOW_ICONS (HICON)LoadIcon(hInstance, IDI_APPLICATION)
#define WINDOW_BRUSH (HBRUSH)(COLOR_WINDOW + 1)
#define WINDOW_CNAME (LPCWSTR)L"Untitled Note"
#define PX_WIN_WIDTH 650
#define PX_WIN_HEIGHT 600
#define WMSG_MIN 0
#define WMSG_MAX 0
#define EXTRA_BYTES 0
#define MSG_INTERVAL_IN_MS 65

#endif /* LIVELY */