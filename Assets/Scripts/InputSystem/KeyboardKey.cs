namespace Unchord
{
    public enum KeyboardKey
    {
        /*
            주의 사항
            - Microsoft C/C++의 Virtual Key Codes Specification에 맞춰 제작한 열거형이므로, 열거형 이름의 순서를 절대 임의로 변경해서는 안 된다.
            - Microsoft C/C++ Virtual Key Codes Documentation: (https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes)
        */

        /*
            Keyboard and Mouse Inputs
        */
        VK_NONE = 0x00,
        VK_LBUTTON = 0x01, // 마우스 왼쪽 클릭
        VK_RBUTTON, // 마우스 오른쪽 클릭
        VL_CANCEL, // 알 수 없음
        VK_MBUTTON, // 마우스 휠 클릭
        VK_XBUTTON1, // 마우스 4번 버튼 (일반적으로는 엄지 손가락으로 누를 수 있음, 오른손잡이 기준)
        VK_XBUTTON2, // 마우스 5번 버튼 (일반적으로는 엄지 손가락으로 누를 수 있음, 오른손잡이 기준)

        VK_BACK = 0x07, // Backspace 키
        VK_TAB,

        VK_CLEAR = 0x0C, // 알 수 없음
        VK_RETURN, // 엔터 키

        VK_SHIFT = 0x10, // Shift 키 (좌/우 모든 Shift가 이 코드에 반응하는 키보드도 있고, 그렇지 않은 키보드도 있음.)
        VK_CONTROL, // Ctrl 키 (좌/우 모든 Ctrl이 이 코드에 반응하는 키보드도 있고, 그렇지 않은 키보드도 있음.)
        VK_MENU, // Alt 키 (좌/우 모든 Alt가 이 코드에 반응하는 키보드도 있고, 그렇지 않은 키보드도 있음.)
        VK_PAUSE, // 일시 정지 키
        VK_CAPITAL, // 알 수 없음
        VK_KANA = 0x15, VK_HANGUEL = 0x15, VK_HANGUL = 0x15, // 한/영 전환
        VK_IME_ON, // 알 수 없음
        VK_JUNJA, // 알 수 없음
        VK_FINAL, // 알 수 없음
        VK_HANJA = 0x19, VK_KANJI = 0x19, // 한자 키
        VK_IME_OFF, // 알 수 없음
        VK_ESCAPE, // ESC
        VK_CONVERT, // 알 수 없음
        VK_NONCONVERT, // 알 수 없음
        VK_ACCEPT, // 알 수 없음
        VK_MODECHANGE, // 알 수 없음
        VK_SPACE,
        VK_PRIOR, // Page Up
        VK_NEXT, // Page Down
        VK_END,
        VK_HOME,
        VK_LEFT, VK_UP, VK_RIGHT, VK_DOWN, // 방향키
        VK_SELECT, // 알 수 없음
        VK_PRINT, // 알 수 없음
        VK_EXECUTE, // 알 수 없음
        VK_SNAPSHOT, // 알 수 없음
        VK_INSERT,
        VK_DELETE,
        VK_HELP, // 도움말 키

        // 숫자 (키보드에 특수문자 붙어있는 숫자들)
        VK_0, VK_1, VK_2, VK_3, VK_4, VK_5, VK_6, VK_7, VK_8, VK_9,

        VK_A = 0x41, VK_B, VK_C, VK_D, VK_E, VK_F, VK_G, VK_H, VK_I, VK_J, VK_K, VK_L, VK_M,
        VK_N, VK_O, VK_P, VK_Q, VK_R, VK_S, VK_T, VK_U, VK_V, VK_W, VK_X, VK_Y, VK_Z,

        VK_LWIN, // 왼쪽 윈도우 키
        VK_RWIN, // 오른쪽 윈도우 키 (없는 키보드도 있음)
        VK_APPS, // 응용 프로그램 키, 메뉴 키, 또는 문서 키

        VK_SLEEP = 0x5F, // 절전 모드 키

        // 키패드 숫자
        VK_NUMPAD0, VK_NUMPAD1, VK_NUMPAD2, VK_NUMPAD3, VK_NUMPAD4,
        VK_NUMPAD5, VK_NUMPAD6, VK_NUMPAD7, VK_NUMPAD8, VK_NUMPAD9,

        // 키패드 연산자
        VK_MULTIPLY, VK_ADD, VK_SEPARATOR, VK_SUBTRACT, VK_DECIMAL, VK_DIVIDE,

        VK_F1, VK_F2, VK_F3, VK_F4, VK_F5, VK_F6, VK_F7, VK_F8, VK_F9, VK_F10,
        VK_F11, VK_F12, VK_F13, VK_F14, VK_F15, VK_1F6, VK_F17, VK_F18, VK_F19, VK_F20,
        VK_F21, VK_F22, VK_F23, VK_F24,

        VK_NUMLOCK = 0x90,
        VK_SCROLL, // Scroll Lock

        VK_LSHIFT = 0xA0,
        VK_RSHIFT,
        VK_LCONTROL,
        VK_RCONTROL,
        VK_LMENU, // 왼쪽 Alt 키
        VK_RMENU, // 오른쪽 Alt 키

        VK_BROWSER_BACK, // 브라우저 창 뒤로 가기 키
        VK_BROWSER_FORWARD, // 브라우저 창 앞으로 가기 키
        VK_BROWSER_REFRESH, // 브라우저 새로 고침 키
        VK_BROWSER_STOP, // 알 수 없음
        VK_BROWSER_SEARCH, // 알 수 없음
        VK_BROWSER_FAVORITES, // 브라우저 즐겨 찾기 키
        VK_BROWSER_HOME, // 브라우저 탐색 키 (브라우저 열림)
        VK_VOLUME_MUTE, // 볼륨 음소거
        VK_VOLUME_DOWN, // 볼륨 하강
        VK_VOLUME_UP, // 볼륨 상승
        VK_MEDIA_NEXT_TRACK, // 다음 음악(또는 영화 등) 재생
        VK_MEDIA_PREV_TRACK, // 이전 음악(또는 영화 등) 재생
        VK_MEDIA_STOP, // 음악(또는 영화 등) 멈춤
        VK_LAUNCH_MAIL, // 알 수 없음
        VK_LAUNCH_MEDIA_SELECT, // 알 수 없음
        VK_LAUNCH_APP1, // 응용 프로그램 실행 (1)
        VK_LAUNCH_APP2, // 응용 프로그램 실행 (2)

        VK_OEM_1 = 0xBA, // ; 키
        VK_OEM_PLUS, // = 키
        VK_OEM_COMMA, // , 키
        VK_OEM_MINUS, // - 키 (키 패드의 - 아님)
        VK_OEM_PERIOD, // . 키
        VK_OEM_2, // / 키 (슬래시 키)
        VK_OEM_3, // ` 키 (물결표 키)

        VK_OEM_4 = 0xDB, // [ 키
        VK_OEM_5, //  \ 키 (역슬래시 키)
        VK_OEM_6, // ] 키
        VK_OEM_7, // ' 키 (작은 따옴표 키)
        VK_OEM_8, // 알 수 없음

        VK_OEM_102 = 0xE2, // 알 수 없음

        VK_PROCESSKEY = 0xE5, // 알 수 없음

        VK_PACKET = 0xE7, // 알 수 없음

        // 아주 오래된 Low-level한 컴퓨터나 서버용 컴퓨터 등에서 사용하는 키 목록인 듯. 잘 모르겠다.
        VK_ATTN = 0xF6, // Attention interrupt key, 알 수 없음
        VK_CRSEL, // 알 수 없음
        VK_EXSEL, // 알 수 없음
        VK_EREOF, // 알 수 없음
        VK_PLAY, // 알 수 없음
        VK_ZOOM, // 알 수 없음
        VK_NONAME, // 알 수 없음
        VK_PA1, // 알 수 없음
        VK_OEM_CLEAR, // 알 수 없음
    }
}