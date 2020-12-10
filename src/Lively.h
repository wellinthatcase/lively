// ----------------------------------------------- //
//		    © 2020 - All rights reserved.          // 
// ----------------------------------------------- //

#pragma once
#pragma comment(lib, "gdi32.lib")
#pragma comment(lib, "User32.lib")

#ifndef UNICODE
#error Unicode support is required.
#endif 

// #include <pplawait.h>
// #include <concurrencysal.h>

#include <string>
#include <stdio.h>
#include <Windows.h>
#include <msclr\marshal_cppstd.h>

#ifndef LIVELY
#define LIVELY

namespace Lively {
	using namespace System;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::ComponentModel;

	public ref class Lively : public System::Windows::Forms::Form
	{
	public:
		Lively(void)
		{
			InitializeComponent();
		}

	protected:
		~Lively()
		{
			if (components) delete components;
		}

	private: System::Windows::Forms::Panel^ TitleBar;
	private: System::Windows::Forms::PictureBox^ eXitButton;
	private: System::Windows::Forms::PictureBox^ Minimize;
	private: System::Windows::Forms::PictureBox^ NewNoteHeader;
	private: System::Windows::Forms::Panel^ UnderlineNoteName;

	private: System::Windows::Forms::TextBox^ NoteTitleInput;


	private: System::Windows::Forms::PictureBox^ LivelyLogo;
	private: System::Windows::Forms::Label^ CopyrightDisclaimer;
	private: System::Windows::Forms::PictureBox^ SubmitNoteName;
	private: System::Windows::Forms::PictureBox^ SmallFailureMsg;
	private: System::Windows::Forms::Label^ ErrMessageContent;

	private: System::Windows::Forms::PictureBox^ LongFailureMsg;
	private: System::Windows::Forms::PictureBox^ DiscordLink;


	private:
		System::ComponentModel::Container^ components;

		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^ resources = (gcnew System::ComponentModel::ComponentResourceManager(Lively::typeid));
			this->TitleBar = (gcnew System::Windows::Forms::Panel());
			this->LivelyLogo = (gcnew System::Windows::Forms::PictureBox());
			this->Minimize = (gcnew System::Windows::Forms::PictureBox());
			this->eXitButton = (gcnew System::Windows::Forms::PictureBox());
			this->NewNoteHeader = (gcnew System::Windows::Forms::PictureBox());
			this->UnderlineNoteName = (gcnew System::Windows::Forms::Panel());
			this->NoteTitleInput = (gcnew System::Windows::Forms::TextBox());
			this->CopyrightDisclaimer = (gcnew System::Windows::Forms::Label());
			this->SubmitNoteName = (gcnew System::Windows::Forms::PictureBox());
			this->SmallFailureMsg = (gcnew System::Windows::Forms::PictureBox());
			this->ErrMessageContent = (gcnew System::Windows::Forms::Label());
			this->LongFailureMsg = (gcnew System::Windows::Forms::PictureBox());
			this->DiscordLink = (gcnew System::Windows::Forms::PictureBox());
			this->TitleBar->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->LivelyLogo))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Minimize))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->eXitButton))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->NewNoteHeader))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->SubmitNoteName))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->SmallFailureMsg))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->LongFailureMsg))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->DiscordLink))->BeginInit();
			this->SuspendLayout();
			// 
			// TitleBar
			// 
			this->TitleBar->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(45)), static_cast<System::Int32>(static_cast<System::Byte>(45)),
				static_cast<System::Int32>(static_cast<System::Byte>(48)));
			this->TitleBar->Controls->Add(this->LivelyLogo);
			this->TitleBar->Controls->Add(this->Minimize);
			this->TitleBar->Controls->Add(this->eXitButton);
			this->TitleBar->Location = System::Drawing::Point(-3, -3);
			this->TitleBar->Name = L"TitleBar";
			this->TitleBar->Size = System::Drawing::Size(656, 47);
			this->TitleBar->TabIndex = 0;
			this->TitleBar->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::MoveWindow);
			// 
			// LivelyLogo
			// 
			this->LivelyLogo->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"LivelyLogo.Image")));
			this->LivelyLogo->Location = System::Drawing::Point(15, 10);
			this->LivelyLogo->Name = L"LivelyLogo";
			this->LivelyLogo->Size = System::Drawing::Size(42, 37);
			this->LivelyLogo->TabIndex = 7;
			this->LivelyLogo->TabStop = false;
			// 
			// Minimize
			// 
			this->Minimize->Cursor = System::Windows::Forms::Cursors::Hand;
			this->Minimize->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"Minimize.Image")));
			this->Minimize->Location = System::Drawing::Point(565, 19);
			this->Minimize->Name = L"Minimize";
			this->Minimize->Size = System::Drawing::Size(38, 25);
			this->Minimize->TabIndex = 1;
			this->Minimize->TabStop = false;
			this->Minimize->MouseClick += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::MinimizeApplication);
			// 
			// eXitButton
			// 
			this->eXitButton->Cursor = System::Windows::Forms::Cursors::Hand;
			this->eXitButton->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"eXitButton.Image")));
			this->eXitButton->Location = System::Drawing::Point(609, 10);
			this->eXitButton->Name = L"eXitButton";
			this->eXitButton->Size = System::Drawing::Size(40, 34);
			this->eXitButton->TabIndex = 0;
			this->eXitButton->TabStop = false;
			this->eXitButton->MouseClick += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::ExitApplication);
			// 
			// NewNoteHeader
			// 
			this->NewNoteHeader->BackColor = System::Drawing::Color::Transparent;
			this->NewNoteHeader->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"NewNoteHeader.Image")));
			this->NewNoteHeader->Location = System::Drawing::Point(12, 50);
			this->NewNoteHeader->Name = L"NewNoteHeader";
			this->NewNoteHeader->Size = System::Drawing::Size(298, 144);
			this->NewNoteHeader->TabIndex = 2;
			this->NewNoteHeader->TabStop = false;
			// 
			// UnderlineNoteName
			// 
			this->UnderlineNoteName->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(240)),
				static_cast<System::Int32>(static_cast<System::Byte>(236)), static_cast<System::Int32>(static_cast<System::Byte>(228)));
			this->UnderlineNoteName->Location = System::Drawing::Point(26, 256);
			this->UnderlineNoteName->Name = L"UnderlineNoteName";
			this->UnderlineNoteName->Size = System::Drawing::Size(298, 4);
			this->UnderlineNoteName->TabIndex = 3;
			// 
			// NoteTitleInput
			// 
			this->NoteTitleInput->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(47)), static_cast<System::Int32>(static_cast<System::Byte>(47)),
				static_cast<System::Int32>(static_cast<System::Byte>(48)));
			this->NoteTitleInput->BorderStyle = System::Windows::Forms::BorderStyle::None;
			this->NoteTitleInput->Font = (gcnew System::Drawing::Font(L"Segoe UI", 17.25F));
			this->NoteTitleInput->ForeColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(240)), static_cast<System::Int32>(static_cast<System::Byte>(236)),
				static_cast<System::Int32>(static_cast<System::Byte>(228)));
			this->NoteTitleInput->Location = System::Drawing::Point(26, 224);
			this->NoteTitleInput->MaxLength = 255;
			this->NoteTitleInput->Name = L"NoteTitleInput";
			this->NoteTitleInput->Size = System::Drawing::Size(298, 31);
			this->NoteTitleInput->TabIndex = 4;
			this->NoteTitleInput->TextChanged += gcnew System::EventHandler(this, &Lively::LegalizeCharacters);
			// 
			// CopyrightDisclaimer
			// 
			this->CopyrightDisclaimer->AutoSize = true;
			this->CopyrightDisclaimer->BackColor = System::Drawing::Color::Transparent;
			this->CopyrightDisclaimer->Font = (gcnew System::Drawing::Font(L"Segoe UI", 9.75F, System::Drawing::FontStyle::Italic, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->CopyrightDisclaimer->ForeColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(240)),
				static_cast<System::Int32>(static_cast<System::Byte>(236)), static_cast<System::Int32>(static_cast<System::Byte>(228)));
			this->CopyrightDisclaimer->Location = System::Drawing::Point(235, 489);
			this->CopyrightDisclaimer->Name = L"CopyrightDisclaimer";
			this->CopyrightDisclaimer->Size = System::Drawing::Size(167, 17);
			this->CopyrightDisclaimer->TabIndex = 7;
			this->CopyrightDisclaimer->Text = L"© 2020 - All rights reserved.";
			// 
			// SubmitNoteName
			// 
			this->SubmitNoteName->BackColor = System::Drawing::Color::Transparent;
			this->SubmitNoteName->Cursor = System::Windows::Forms::Cursors::Hand;
			this->SubmitNoteName->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"SubmitNoteName.Image")));
			this->SubmitNoteName->Location = System::Drawing::Point(340, 224);
			this->SubmitNoteName->Name = L"SubmitNoteName";
			this->SubmitNoteName->Size = System::Drawing::Size(34, 36);
			this->SubmitNoteName->TabIndex = 8;
			this->SubmitNoteName->TabStop = false;
			this->SubmitNoteName->Visible = false;
			// 
			// SmallFailureMsg
			// 
			this->SmallFailureMsg->BackColor = System::Drawing::Color::Transparent;
			this->SmallFailureMsg->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"SmallFailureMsg.Image")));
			this->SmallFailureMsg->Location = System::Drawing::Point(26, 266);
			this->SmallFailureMsg->Name = L"SmallFailureMsg";
			this->SmallFailureMsg->Size = System::Drawing::Size(205, 65);
			this->SmallFailureMsg->TabIndex = 9;
			this->SmallFailureMsg->TabStop = false;
			this->SmallFailureMsg->Visible = false;
			// 
			// ErrMessageContent
			// 
			this->ErrMessageContent->AutoSize = true;
			this->ErrMessageContent->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(0)), static_cast<System::Int32>(static_cast<System::Byte>(168)),
				static_cast<System::Int32>(static_cast<System::Byte>(243)));
			this->ErrMessageContent->Font = (gcnew System::Drawing::Font(L"Segoe UI", 10.25F));
			this->ErrMessageContent->ForeColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(45)),
				static_cast<System::Int32>(static_cast<System::Byte>(45)), static_cast<System::Int32>(static_cast<System::Byte>(48)));
			this->ErrMessageContent->Location = System::Drawing::Point(38, 289);
			this->ErrMessageContent->Name = L"ErrMessageContent";
			this->ErrMessageContent->Size = System::Drawing::Size(29, 19);
			this->ErrMessageContent->TabIndex = 10;
			this->ErrMessageContent->Text = L"foo";
			this->ErrMessageContent->Visible = false;
			// 
			// LongFailureMsg
			// 
			this->LongFailureMsg->BackColor = System::Drawing::Color::Transparent;
			this->LongFailureMsg->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"LongFailureMsg.Image")));
			this->LongFailureMsg->Location = System::Drawing::Point(26, 266);
			this->LongFailureMsg->Name = L"LongFailureMsg";
			this->LongFailureMsg->Size = System::Drawing::Size(310, 75);
			this->LongFailureMsg->TabIndex = 11;
			this->LongFailureMsg->TabStop = false;
			this->LongFailureMsg->Visible = false;
			// 
			// DiscordLink
			// 
			this->DiscordLink->BackColor = System::Drawing::Color::Transparent;
			this->DiscordLink->Cursor = System::Windows::Forms::Cursors::Hand;
			this->DiscordLink->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"DiscordLink.Image")));
			this->DiscordLink->Location = System::Drawing::Point(598, 472);
			this->DiscordLink->Name = L"DiscordLink";
			this->DiscordLink->Size = System::Drawing::Size(36, 34);
			this->DiscordLink->TabIndex = 12;
			this->DiscordLink->TabStop = false;
			this->DiscordLink->MouseClick += gcnew System::Windows::Forms::MouseEventHandler(this, &Lively::JoinDiscordFromLink);
			// 
			// Lively
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(30)), static_cast<System::Int32>(static_cast<System::Byte>(30)),
				static_cast<System::Int32>(static_cast<System::Byte>(30)));
			this->BackgroundImage = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"$this.BackgroundImage")));
			this->ClientSize = System::Drawing::Size(642, 515);
			this->Controls->Add(this->DiscordLink);
			this->Controls->Add(this->LongFailureMsg);
			this->Controls->Add(this->ErrMessageContent);
			this->Controls->Add(this->SmallFailureMsg);
			this->Controls->Add(this->SubmitNoteName);
			this->Controls->Add(this->CopyrightDisclaimer);
			this->Controls->Add(this->NoteTitleInput);
			this->Controls->Add(this->UnderlineNoteName);
			this->Controls->Add(this->NewNoteHeader);
			this->Controls->Add(this->TitleBar);
			this->Cursor = System::Windows::Forms::Cursors::Default;
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::None;
			this->Icon = (cli::safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));
			this->Name = L"Lively";
			this->Text = L"Lively";
			this->TitleBar->ResumeLayout(false);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->LivelyLogo))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Minimize))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->eXitButton))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->NewNoteHeader))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->SubmitNoteName))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->SmallFailureMsg))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->LongFailureMsg))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->DiscordLink))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}

	private: System::Void MoveWindow(
		_In_opt_ System::Object^ sender,
		_In_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);

		if (events->Button == System::Windows::Forms::MouseButtons::Left)
		{
			ReleaseCapture();
			SendMessage(
				static_cast<HWND>(this->Handle.ToPointer()),
				WM_NCLBUTTONDOWN,
				HTCAPTION,
				NULL
			);
		}
	}

	private: System::Void ExitApplication(
		_In_opt_ System::Object^ sender,
		_In_opt_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);
		UNREFERENCED_PARAMETER(events);

		using MessageBoxOT = System::Windows::Forms::MessageBox;
		using DialogResult = System::Windows::Forms::DialogResult;
		using MsgBoxWIcons = System::Windows::Forms::MessageBoxIcon;
		using MsgBoxButton = System::Windows::Forms::MessageBoxButtons;

		DialogResult res = MessageBoxOT::Show(
			"Are you sure you wanna quit?",
			"Confirm",
			MsgBoxButton::YesNoCancel,
			MsgBoxWIcons::Question
		);

		if (res == DialogResult::Yes)
		{
			Application::Exit();
		}
	}

	private: System::Void MinimizeApplication(
		_In_opt_ System::Object^ sender,
		_In_opt_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);
		UNREFERENCED_PARAMETER(events);

		this->WindowState = System::Windows::Forms::FormWindowState::Minimized;
	}

	private: System::Void MessageErrTxtTextVisible(_In_ bool visible)
	{
		this->ErrMessageContent->Visible = visible;
	}

	private: System::Void SmallMessageErrVisible(_In_ bool visible)
	{
		this->MessageErrTxtTextVisible(visible);
		this->SmallFailureMsg->Visible = visible;
	}

	private: System::Void LongMessageErrVisible(_In_ bool visible)
	{
		this->MessageErrTxtTextVisible(visible);
		this->LongFailureMsg->Visible = visible;
	}

	private: System::Void SetErrTxtText(_In_ System::String^ txt)
	{
		this->SubmitNoteName->Visible    = false;
		this->ErrMessageContent->Text    = txt; 
		this->ErrMessageContent->Visible = true;
	}

	private: System::Void LegalizeCharacters(
		_In_opt_ System::Object^ sender, 
		_In_opt_ System::EventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);
		UNREFERENCED_PARAMETER(events);

		const auto txt = this->NoteTitleInput->Text; 
		this->ErrMessageContent->BringToFront();

		if (txt == "NUL"
			|| txt == "AUX"
			|| txt == "PRN"
			|| txt == "CON")
		{
			this->SetErrTxtText(System::String::Format("   {0} is a forbidden name.", txt));
			this->SmallMessageErrVisible(true);

			return; 
		}

		if (txt->EndsWith("."))
		{
			this->SetErrTxtText(L"Note titles can't end with a period.");
			this->LongMessageErrVisible(true);

			return;
		}

		if (txt->Length == 4
			&& txt->StartsWith("COM") || txt->StartsWith("LPT")
			&& std::isdigit(msclr::interop::marshal_as<std::string>(txt).back()))
		{
			const auto id = txt->Substring(0, 3);
			this->SetErrTxtText(System::String::Format("   {0}(0-9) is forbidden.", id));
			this->SmallMessageErrVisible(true);

			return;
		}

		if (txt->Contains("<")
			|| txt->Contains(">")
			|| txt->Contains(":")
			|| txt->Contains("|")
			|| txt->Contains("?")
			|| txt->Contains("*")
			|| txt->Contains("/")
			|| txt->Contains(R"(\)")
			|| txt->Contains(R"(")"))
		{
			this->LongMessageErrVisible(true);
			this->SetErrTxtText(L"<, >, :, ?, *, /, and \\ are forbidden characters.");

			return; 
		}

		this->LongMessageErrVisible(false);
		this->SmallMessageErrVisible(false);
		this->MessageErrTxtTextVisible(false);
		this->SubmitNoteName->Visible = bool(txt->Length);
	}

	/* In the future, this code needs to be outsourced to a DLL. 
	   Projects with the /CLR or /CLR:pure command-line flag are incompatible
	   with <pplawait.h> & <concurrencysal.h>

	   If you're curious, this snippet of code was designed 
	   to only show the Discord logo if the user has internet connectivity. 

	   Pinging is one of the more reliable methods of checking connectivity,
	   however, there really is no truly reliable way of doing that. 

	   I don't request /generate_204/ because it'll download around 4KB of data,
	   which is 4KB of data I'll never use; verses 32 bytes from ping. 

	private: Concurrency::task<System::Void> RevealDiscordLinkIfInternet(
		_In_opt_ System::Object^ sender,
		_In_opt_ System::EventArgs^ events)
	{
		UNREFERENCED_PARAMETER(sender);
		UNREFERENCED_PARAMETER(events);

		this->DiscordLink->Visible = bool(WinExec("ping 8.8.8.8 > bin/connected_maybe.dat", SW_HIDE));
	}

	private: System::Void ShowDiscordLink(
		_In_opt_ System::Object^ sender, 
		_In_opt_ System::EventArgs^ events
	) {	
		Concurrency::await_ready(this->RevealDiscordLinkIfInternet(sender, events));
	}
	*/

	private: System::Void JoinDiscordFromLink(
		_In_opt_ System::Object^ sender, 
		_In_opt_ System::Windows::Forms::MouseEventArgs^ events
	) {
		UNREFERENCED_PARAMETER(sender);
		UNREFERENCED_PARAMETER(events);

		/*
			I know throwing in a static string of the Discord is vulnerable
			to web scrapers, though I'm not too worried of a raid. 
		*/

		System::Diagnostics::Process::Start("https://discord.gg/efQ3nEUveQ");
	}
};
}
#endif
