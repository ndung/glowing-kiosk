program ACOSReadWrite;

uses
  Forms,
  ReadWrite in 'ReadWrite.pas' {MainReadWrite};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TMainReadWrite, MainReadWrite);
  Application.Run;
end.
