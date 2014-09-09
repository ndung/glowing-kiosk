program SLE4418_4428Sample;

uses
  Forms,
  SLE4418_4428 in 'SLE4418_4428.pas' {SLE4418_4428Main};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TSLE4418_4428Main, SLE4418_4428Main);
  Application.Run;
end.
