#pragma implicitwith disable
page 90101 "BC LRS Services"
{
    PageType = List;
    UsageCategory = Lists;
    ApplicationArea = All;
    SourceTable = "BC LRS Service";
    Caption = 'BC Local Resource Services';
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
        area(Processing)
        {
            action(Resources)
            {
                Caption = 'Registered Resources';
                ApplicationArea = All;
                Image = Resource;
                RunObject = page "BC LRS service Resources";
                //RunPageLink = "Local Service Code" = field(Code);
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
