page 90110 "BC LRS Service Setup Lines"
{
    ApplicationArea = All;
    Caption = 'Service Setup Lines';
    PageType = List;
    SourceTable = "BC LRS Service Setup Line";
    UsageCategory = None;

    layout
    {
        area(content)
        {
            repeater(General)
            {

                field("Setup Variable Name"; Rec."Setup Variable Name")
                {
                    ToolTip = 'Specifies the value of the Setup Variable Name field.';
                }
                field("Setup Value"; Rec."Setup Value")
                {
                    ToolTip = 'Specifies the value of the Setup Value field.';
                }
                field("Is Sensitive"; Rec."Is Sensitive")
                {
                    ToolTip = 'Specifies the value of the Is Sensitive field.';
                    Editable = false;
                }
            }
        }
    }
    actions
    {
        area(Navigation)
        {
            action(act_showsecret)
            {
                ApplicationArea = All;
                Caption = 'Show Secret';
                ToolTip = 'Display the secret text';
                Image = EncryptionKeys;
                trigger OnAction()
                begin
                    if Rec."Is Sensitive" then
                        Message(Rec.GetSensitiveData());
                end;
            }
        }
    }
}
