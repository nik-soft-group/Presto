﻿namespace NiksoftCore.FormBuilder.Service
{
    public interface IFormBuilderService
    {
        IFormCategroyService iFormCategroyServ { get; set; }
        IFormService iFormServ { get; set; }
        IFormControlService iFormControlServ { get; set; }
        IListItemService iListItemServ { get; set; }
        IFormAnswerService iFormAnswerServ { get; set; }
    }

    public class FormBuilderService
    {
        public IFormCategroyService iFormCategroyServ { get; set; }
        public IFormService iFormServ { get; set; }
        public IFormControlService iFormControlServ { get; set; }
        public IListItemService iListItemServ { get; set; }
        public IFormAnswerService iFormAnswerServ { get; set; }

        public FormBuilderService(string connection)
        {
            IFbUnitOfWork uow = new FbDbContext(connection);
            iFormCategroyServ = new FormCategroyService(uow);
            iFormServ = new FormService(uow);
            iFormControlServ = new FormControlService(uow);
            iListItemServ = new ListItemService(uow);
            iFormAnswerServ = new FormAnswerService(uow);
        }
    }
}