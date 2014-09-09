program AccountSample;

uses
  Forms,
  Account in 'Account.pas' {MainAccount};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainAccount, MainAccount);
  Application.Run;
end.
