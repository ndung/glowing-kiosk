program ConfigureATRSample;

uses
  Forms,
  GetATR in 'GetATR.pas' {MainConfigureATR};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainConfigureATR, MainConfigureATR);
  Application.Run;
end.
