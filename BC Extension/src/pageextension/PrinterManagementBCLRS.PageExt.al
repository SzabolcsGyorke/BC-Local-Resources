pageextension 90100 "Printer Management BCLRS" extends "Printer Management"
{
    AdditionalSearchTerms = 'universal printers,universalprint';
    PromotedActionCategories = 'New, Process, Report, Manage, Email Print, Universal Print, Local Printers';
    actions
    {
        addlast(Creation)
        {
            action(act_getprinters_BCLRS)
            {
                ApplicationArea = all;
                Caption = 'Get Local Printers';
                ToolTip = 'Display a list of local printer connected via local BC resources.';
                Image = Print;
                Promoted = true;
                PromotedCategory = Category7;
                PromotedOnly = true;
                trigger OnAction()
                var
                    BCLRSManagement: Codeunit "BC LRS Management";
                begin
                    BCLRSManagement.AddToSystemPrinters();
                end;
            }
        }
    }
}

