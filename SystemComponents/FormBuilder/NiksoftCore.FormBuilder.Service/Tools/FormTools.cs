namespace NiksoftCore.FormBuilder.Service
{
    public static class FormTools
    {
        public static string GetControlName(this ControlType type)
        {
            switch (type)
            {
                case ControlType.TextBox:
                    return "Text Box";
                case ControlType.TextArea:
                    return "Text Area";
                case ControlType.DropDownList:
                    return "Drop Down List";
                case ControlType.CheckBoxList:
                    return "Check Box List";
                case ControlType.RadioButtonList:
                    return "Radio Button List";
                case ControlType.FileUpload:
                    return "Upload File";
                case ControlType.ExtenalText:
                    return "External Text String";
                default:
                    return "Unknown";
            }
        }

        public static bool ShowListItemBtn(this ControlType type)
        {
            switch (type)
            {
                case ControlType.DropDownList:
                    return true;
                case ControlType.CheckBoxList:
                    return true;
                case ControlType.RadioButtonList:
                    return true;
                default:
                    return false;
            }
        }

        public static int GetControlNo(this ControlType type)
        {
            switch (type)
            {
                case ControlType.TextBox:
                    return 1;
                case ControlType.TextArea:
                    return 2;
                case ControlType.DropDownList:
                    return 3;
                case ControlType.CheckBoxList:
                    return 4;
                case ControlType.RadioButtonList:
                    return 5;
                case ControlType.FileUpload:
                    return 6;
                case ControlType.ExtenalText:
                    return 7;
                default:
                    return 0;
            }
        }


    }
}
