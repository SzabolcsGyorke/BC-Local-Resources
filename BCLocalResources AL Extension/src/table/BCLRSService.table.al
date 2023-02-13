table 90101 "BC LRS Service"
{
    DataClassification = CustomerContent;
    LookupPageId = 90101;
    fields
    {
        field(1; "Code"; Code[250])
        {
            Caption = 'Code';
            DataClassification = CustomerContent;
        }


        field(3; "No. of Pending Entries"; Integer)
        {
            Caption = 'No. of Pending Entries';
            Editable = false;
            FieldClass = FlowField;
            CalcFormula = Count("BC LRS Entry" where("Local Service Code" = field(Code), "Entry Completed" = const(false)));
        }

        field(4; "Last Heartbeat"; DateTime)
        {
            Caption = 'Last Heartbeat';
            DataClassification = CustomerContent;
        }

        field(5; "Last Resource Update"; DateTime)
        {
            Caption = 'Last Resource Update';
            DataClassification = CustomerContent;
        }

        field(6; "Request Resource Update"; Boolean)
        {
            Caption = 'Request Resource Update';
            DataClassification = CustomerContent;
        }

        field(7; "Local Printers"; Integer)
        {
            Caption = 'Local Printers';
            Editable = false;
            FieldClass = FlowField;
            CalcFormula = Count("BC LRS Service Resource" where("Local Service Code" = field(Code), "Resource Type" = const(Printer)));
        }
        field(8; "Local Folders"; Integer)
        {
            Caption = 'Local Folders';
            Editable = false;
            FieldClass = FlowField;
            CalcFormula = Count("BC LRS Service Resource" where("Local Service Code" = field(Code), "Resource Type" = filter(<> Printer)));
        }


    }

    keys
    {
        key(PK; "Code")
        {
            Clustered = true;
        }
    }
    var
        StatusOption: Option Running,"Connection Lost";

    procedure GetStatus(): Option
    var
        BCLRSSetup: Record "BC LRS Setup";
        lasthbinminutes: Integer;
    begin
        BCLRSSetup.get();
        if BCLRSSetup."Heart Beat Keep Alive (min)" = 0 then
            exit(StatusOption::Running);

        if "Last Heartbeat" = 0DT then
            exit(StatusOption::"Connection Lost");

        lasthbinminutes := Round((CurrentDateTime() - "Last Heartbeat") / 1000 / 60, 1);

        if lasthbinminutes <= BCLRSSetup."Heart Beat Keep Alive (min)" then
            exit(StatusOption::Running)
        else
            exit(StatusOption::"Connection Lost");

    end;

    procedure GetStatusTextAndStyle(var statustext: Text): Text
    begin
        case GetStatus() of
            StatusOption::"Connection Lost":
                begin
                    statustext := Format(StatusOption::"Connection Lost");
                    exit('Unfavorable')
                end;
            StatusOption::Running:
                begin
                    statustext := Format(StatusOption::Running);
                    exit('Favorable')
                end;
        end;

        statustext := 'not initialized';
        exit('Standard');
    end;

}