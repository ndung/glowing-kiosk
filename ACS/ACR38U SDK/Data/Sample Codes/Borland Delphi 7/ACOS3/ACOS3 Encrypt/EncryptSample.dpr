program EncryptSample;

uses
  Forms,
  Encryption in 'Encryption.pas' {MainEncrypt};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainEncrypt, MainEncrypt);
  Application.Run;
end.
