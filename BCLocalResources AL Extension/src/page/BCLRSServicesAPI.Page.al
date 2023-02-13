page 90103 "BCLRSServicesAPI"
{
    APIGroup = 'bclrs';
    APIPublisher = 'bclrs';
    APIVersion = 'v1.0';
    ApplicationArea = All;
    Caption = 'bclrsServicesAPI';
    DelayedInsert = true;
    EntityName = 'serviceResource';
    EntitySetName = 'serviceResources';
    PageType = API;
    SourceTable = "BC LRS Service Resource";

    layout
    {
        area(content)
        {
            repeater(General)
            {
                field(no; Rec."No.")
                {
                    Caption = 'No.';
                }
                field(localServiceCode; Rec."Local Service Code")
                {
                    Caption = 'Local Service Code';
                }
                field(localResourceCode; Rec."Local Resource Code")
                {
                    Caption = 'Local Resource Code';
                }
                field(enabled; Rec.Enabled)
                {
                    Caption = 'Enabled';
                }
                field(resourceType; Rec."Resource Type")
                {
                    Caption = 'Resource Type';
                }
                field(afterImportFileAction; Rec."After Import File Action")
                {
                    Caption = 'After Import File Action';
                }
                field(description; Rec.Description)
                {
                    Caption = 'Description';
                }
                field(landscape; Rec.Landscape)
                {
                    Caption = 'Landscape';
                }
                field(paperHeight; Rec."Paper Height")
                {
                    Caption = 'Printer Paper Height';
                }
                field(paperSize; Rec."Paper Size")
                {
                    Caption = 'Paper Size';
                }
                field(paperTray; Rec."Paper Tray")
                {
                    Caption = 'Paper Tray';
                }
                field(paperWidth; Rec."Paper Width")
                {
                    Caption = 'Printer Paper Width';
                }
                field(useRawPrint; Rec."Use Raw Print")
                {
                    Caption = 'Use Raw Print';
                }
            }
        }
    }
}
