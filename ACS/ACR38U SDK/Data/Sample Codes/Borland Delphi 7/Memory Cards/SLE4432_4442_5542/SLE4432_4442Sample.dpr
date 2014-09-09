program SLE4432_4442Sample;

uses
  Forms,
  SLE4432_4442 in 'SLE4432_4442.pas' {SLE4432_4442Main};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TSLE4432_4442Main, SLE4432_4442Main);
  Application.Run;
end.
