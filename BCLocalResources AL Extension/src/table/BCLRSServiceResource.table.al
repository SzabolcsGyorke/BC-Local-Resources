table 90102 "BC LRS Service Resource"
{
    DataClassification = CustomerContent;
    Caption = 'Local Service Resource';
    LookupPageId = 90102;
    fields
    {
        field(1; "No."; Code[20])
        {
            Caption = 'No.';
            DataClassification = CustomerContent;
        }

        field(2; "Local Service Code"; Code[250])
        {
            Caption = 'Local Service Code';
            DataClassification = CustomerContent;
            TableRelation = "BC LRS Service";
        }

        field(3; "Local Resource Code"; Text[250])
        {
            Caption = 'Local Resource Code';
            DataClassification = CustomerContent;
        }

        field(4; "Resource Type"; Option)
        {
            Caption = 'Resource Type';
            DataClassification = CustomerContent;
            OptionMembers = Printer,"File Queue To Client","File Queue From Client","Command to Client";
            trigger OnValidate()
            var
                defCmdTxt: Label 'COMMAND';
            begin
                if "Resource Type" = "Resource Type"::"File Queue From Client" then
                    "After Import File Action" := "After Import File Action"::"Delete Source + Run Subscriber Action";

                if "Resource Type" = "Resource Type"::"Command to Client" then
                    "Local Resource Code" := defCmdTxt;
            end;
        }


        field(5; Description; Text[250])
        {
            DataClassification = CustomerContent;
            Caption = 'Description';
        }

        field(6; "No. of Pending Entries"; Integer)
        {
            Caption = 'No. of Pending Entries';
            Editable = false;
            FieldClass = FlowField;
            CalcFormula = Count("BC LRS Entry" where("Local Service Code" = field("Local Service Code"), "Local Resource Code" = field("Local Resource Code"), "Entry Completed" = const(false)));
        }


        field(11; "After Import File Action"; Option)
        {
            Caption = 'After Import File Action';
            DataClassification = CustomerContent;
            OptionMembers = " ","Delete Source","Move to Archive","Delete Source + Run Subscriber Action","Move to Archive + Run Subscriber Action";
            trigger OnValidate()
            begin
                if ("After Import File Action" > 0) and ("Resource Type" = "Resource Type"::Printer) then
                    FieldError("After Import File Action");
            end;
        }
        field(12; Enabled; Boolean)
        {
            Caption = 'Enabled';
            DataClassification = CustomerContent;
        }


        field(30; Landscape; Boolean)
        {
            DataClassification = CustomerContent;
            Caption = 'Landscape';
        }
        field(31; "Paper Size"; Enum "Printer Paper Kind")
        {
            Caption = 'Paper Size';
        }

        field(32; "Paper Height"; Decimal)
        {
            Caption = 'Printer Paper Height';
            DecimalPlaces = 0 : 2;
        }


        field(33; "Paper Width"; Decimal)
        {
            Caption = 'Printer Paper Width';
            DecimalPlaces = 0 : 2;
        }

        field(34; "Paper Tray"; Enum "Printer Paper Source Kind")
        {
            Caption = 'Paper Tray';
            DataClassification = CustomerContent;
        }
        field(35; "Use Raw Print"; Boolean)
        {
            Caption = 'Use Raw Print';
            DataClassification = CustomerContent;
        }


    }

    keys
    {
        key(PK; "No.")
        {
            Clustered = true;
        }
        key(Key1; "Local Service Code", "Local Resource Code") { }
    }

    fieldgroups
    {
        fieldgroup("DropDown"; "Local Service Code", "Local Resource Code", Description, Enabled) { }
    }

    trigger OnInsert()
    var
        BCLRSSetup: Record "BC LRS Setup";
        NoSeriesManagement: Codeunit NoSeriesManagement;
    begin
        if "No." = '' then begin
            BCLRSSetup.Get();
            BCLRSSetup.TestField("Service Resource Nos.");
            "No." := NoSeriesManagement.GetNextNo(BCLRSSetup."Service Resource Nos.", WorkDate(), true);
        end;
    end;

    trigger OnDelete()
    begin
        DeleteEntries(false);
    end;

    internal procedure DeleteEntries(askquestion: Boolean)
    var
        BCLRSEntry: Record "BC LRS Entry";
        DeleteEntriesQst: Label 'Do you want to delete all connected entries?';
    begin
        if askquestion then
            if not Confirm(DeleteEntriesQst, false) then exit;

        BCLRSEntry.SetRange("Local Service Code", "Local Service Code");
        BCLRSEntry.SetRange("Local Resource Code", "Local Resource Code");
        BCLRSEntry.DeleteAll(true);
    end;
}