program CreateFileSample;

uses
  Forms,
  CreateFiles in 'CreateFiles.pas' {MainCreateFiles};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainCreateFiles, MainCreateFiles);
  Application.Run;
end.
