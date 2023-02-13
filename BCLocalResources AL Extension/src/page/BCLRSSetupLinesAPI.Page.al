page 90111 "BCLRSSetupLinesAPI"
{
    APIGroup = 'bclrs';
    APIPublisher = 'bclrs';
    APIVersion = 'v1.0';
    ApplicationArea = All;
    Caption = 'bclrsSetupLinesAPI';
    DelayedInsert = true;
    EntityName = 'bclrsSetupLine';
    EntitySetName = 'bclrsSetupLines';
    PageType = API;
    SourceTable = "BC LRS Service Setup Line";

    layout
    {
        area(content)
        {
            repeater(General)
            {
                field(localServiceCode; Rec."Local Service Code")
                {
                    Caption = 'Local Service Code';
                }
                field(setupVariableName; Rec."Setup Variable Name")
                {
                    Caption = 'Setup Variable Name';
                }
                field(setupValue; Rec."Setup Value")
                {
                    Caption = 'Setup Value';
                }
                field(isSensitive; Rec."Is Sensitive")
                {
                    Caption = 'Is Sensitive';
                }
            }
        }
    }
}
