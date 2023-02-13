#pragma implicitwith disable
page 90101 "BC LRS Services"
{
    PageType = List;
    UsageCategory = Lists;
    ApplicationArea = All;
    SourceTable = "BC LRS Service";
    Caption = 'Local Resource Services';
    layout
    {
        area(Content)
        {
            repeater(Group)
            {
                field(Code; Rec.Code)
                {
                    ApplicationArea = All;
                }
                field("No. of Pending Entries"; Rec."No. of Pending Entries")
                {
                    ApplicationArea = All;
                }

                field("Last Heartbeat"; Rec."Last Heartbeat")
                {
                    ApplicationArea = all;
                    Editable = false;
                }

                field(StatusText; StatusText)
                {
                    ApplicationArea = all;
                    Editable = false;
                    Caption = 'Service Status';
                    StyleExpr = StatusStyle;
                }
                field("Local Folders"; Rec."Local Folders")
                {
                    ToolTip = 'Specifies the value of the Local Folders field.';
                    DrillDownPageId = "BC LRS Folders";
                }
                field("Local Printers"; Rec."Local Printers")
                {
                    ToolTip = 'Specifies the value of the Local Printers field.';
                    DrillDownPageId = "BC LRS Printers";
                }
                field("Request Resource Update"; Rec."Request Resource Update")
                {
                    ApplicationArea = all;
                }
                field("Last Resource Update"; Rec."Last Resource Update")
                {
                    ApplicationArea = all;
                    Editable = false;
                }

            }
        }
        area(Factboxes)
        {

        }
    }

    actions
    {
        area(Promoted)
        {
            actionref(act_regrs; Resources) { }
            group(Navigate)
            {
                Caption = 'Service Setup';
                Image = AboutNav;
                actionref(act_config; act_Setup) { }
                actionref(act_export_conf; act_Export_Config) { }
            }
        }
        area(Processing)
        {
            action(Resources)
            {
                Caption = 'Registered Resources';
                ApplicationArea = All;
                Image = Resource;
                RunObject = page "BC LRS service Resources";
                RunPageLink = "Local Service Code" = field(Code);
            }
            action(act_Export_Config)
            {
                ApplicationArea = All;
                Caption = 'Export Config XML';
                ToolTip = 'Export the configuraion to load it to the windows service.';
                Image = CreateXMLFile;
                trigger OnAction()
                var
                    BCLRSManagement: Codeunit "BC LRS Management";
                begin
                    BCLRSManagement.ExportServiceXMLConfig(Rec);
                end;
            }
        }
        area(Navigation)
        {
            action(act_Setup)
            {
                ApplicationArea = All;
                Caption = 'Setup';
                ToolTip = 'Display the local resource setup';
                Image = Setup;
                RunObject = page "BC LRS Service Setup Lines";
                RunPageLink = "Local Service Code" = field(Code);
            }
        }
    }
    var
        [InDataSet]
        StatusStyle: Text;
        StatusText: Text;

    trigger OnAfterGetRecord()

    begin
        StatusStyle := Rec.GetStatusTextAndStyle(StatusText);
    end;
}
#pragma implicitwith restore
