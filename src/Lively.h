// ----------------------------------------------- //
//		    © 2020 - All rights reserved.          // 
// ----------------------------------------------- //

#pragma once
#pragma comment(lib, "gdi32.lib")
#pragma comment(lib, "User32.lib")

#ifndef UNICODE
#define UNICODE
#endif 

#include <Windows.h>

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
	private: System::Windows::Forms::PictureBox^ PassedFilename;
	private: System::Windows::Forms::PictureBox^ InvalidFileName;
	private: System::Windows::Forms::PictureBox^ LivelyLogo;
	private: System::Windows::Forms::Label^ CopyrightDisclaimer;

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
			this->PassedFilename = (gcnew System::Windows::Forms::PictureBox());
			this->InvalidFileName = (gcnew System::Windows::Forms::PictureBox());
			this->CopyrightDisclaimer = (gcnew System::Windows::Forms::Label());
			this->TitleBar->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->LivelyLogo))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Minimize))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->eXitButton))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->NewNoteHeader))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->PassedFilename))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->InvalidFileName))->BeginInit();
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
			this->Minimize->Location = System::Drawing::Point(566, 14);
			this->Minimize->Name = L"Minimize";
			this->Minimize->Size = System::Drawing::Size(27, 31);
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
			this->eXitButton->Size = System::Drawing::Size(40, 27);
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
			this->NoteTitleInput->Name = L"NoteTitleInput";
			this->NoteTitleInput->Size = System::Drawing::Size(298, 31);
			this->NoteTitleInput->TabIndex = 4;
			// 
			// PassedFilename
			// 
			this->PassedFilename->BackColor = System::Drawing::Color::Transparent;
			this->PassedFilename->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"PassedFilename.Image")));
			this->PassedFilename->Location = System::Drawing::Point(331, 224);
			this->PassedFilename->Name = L"PassedFilename";
			this->PassedFilename->Size = System::Drawing::Size(31, 31);
			this->PassedFilename->TabIndex = 5;
			this->PassedFilename->TabStop = false;
			this->PassedFilename->Visible = false;
			// 
			// InvalidFileName
			// 
			this->InvalidFileName->BackColor = System::Drawing::Color::Transparent;
			this->InvalidFileName->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"InvalidFileName.Image")));
			this->InvalidFileName->Location = System::Drawing::Point(331, 224);
			this->InvalidFileName->Name = L"InvalidFileName";
			this->InvalidFileName->Size = System::Drawing::Size(31, 31);
			this->InvalidFileName->TabIndex = 6;
			this->InvalidFileName->TabStop = false;
			this->InvalidFileName->Visible = false;
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
			// Lively
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(30)), static_cast<System::Int32>(static_cast<System::Byte>(30)),
				static_cast<System::Int32>(static_cast<System::Byte>(30)));
			this->BackgroundImage = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"$this.BackgroundImage")));
			this->ClientSize = System::Drawing::Size(642, 515);
			this->Controls->Add(this->CopyrightDisclaimer);
			this->Controls->Add(this->InvalidFileName);
			this->Controls->Add(this->PassedFilename);
			this->Controls->Add(this->NoteTitleInput);
			this->Controls->Add(this->UnderlineNoteName);
			this->Controls->Add(this->NewNoteHeader);
			this->Controls->Add(this->TitleBar);
			this->Cursor = System::Windows::Forms::Cursors::Arrow;
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::None;
			this->Icon = (cli::safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));
			this->Name = L"Lively";
			this->Text = L"Lively";
			this->TitleBar->ResumeLayout(false);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->LivelyLogo))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Minimize))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->eXitButton))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->NewNoteHeader))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->PassedFilename))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->InvalidFileName))->EndInit();
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
};
}
#endif
