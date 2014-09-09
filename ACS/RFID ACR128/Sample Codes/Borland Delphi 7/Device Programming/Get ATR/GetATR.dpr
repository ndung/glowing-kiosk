program GetATR;

uses
  Forms,
  GetATRMain in 'GetATRMain.pas' {MainGetATR};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainGetATR, MainGetATR);
  Application.Run;
end.
