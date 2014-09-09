program AdvancedDevProg;

uses
  Forms,
  AdvDevProg in 'AdvDevProg.pas' {MainAdvDevProg};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainAdvDevProg, MainAdvDevProg);
  Application.Run;
end.
