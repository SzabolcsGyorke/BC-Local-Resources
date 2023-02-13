//***************************************************************
//Credits: Stefan Maroń
//URL: https://stefanmaron.com/2020/08/20/business-central-and-multiline-fields/
//***************************************************************
page 90109 "BC LRS Edit Text"
{
    ApplicationArea = all;
    Caption = 'Edit Text';
    PageType = StandardDialog;

    layout
    {
        area(content)
        {
            usercontrol(LongText; "Microsoft.Dynamics.Nav.Client.WebPageViewer")
            {
                trigger ControlAddInReady(callbackUrl: Text)
                begin
                    IsReady := true;
                    FillAddIn();
                end;

                trigger Callback(data: Text)
                begin
                    EditedText := data;
                end;
            }
        }
    }
    var
        EditedText: Text;
        TipText: Text;
        newCaption: Text;
        EditorHTMLTxt: Label '<textarea Id="TextArea" maxlength="%2" style="width:100%;height:100%;resize: none; font-family:"Segoe UI", "Segoe WP", Segoe, device-segoe, Tahoma, Helvetica, Arial, sans-serif !important; font-size: 10.5pt !important;" OnChange="window.parent.WebPageViewerHelper.TriggerCallback(document.getElementById(''TextArea'').value)">%1</textarea>', Comment='%1: text to edit', Locked=true;
        IsReady: Boolean;

    trigger OnAfterGetCurrRecord()
    var
        notification: Notification;
    begin
        if IsReady then
            FillAddIn();

        if TipText <> '' then begin
            notification.Message(TipText);
            notification.Scope := NotificationScope::LocalScope;
            notification.Send();
        end;

        if newCaption <> '' then
            CurrPage.Caption(newCaption);
    end;

    local procedure FillAddIn()
    begin
        CurrPage.LongText.SetContent(StrSubstNo(EditorHTMLTxt, EditedText, MaxStrLen(EditedText)));
    end;

    procedure SetText(inText: Text)
    begin
        EditedText := inText;
    end;

    procedure GetText(): Text
    begin
        exit(EditedText);
    end;

    procedure SetTip(_TipText: Text)
    begin
        TipText := _TipText;
    end;

}
