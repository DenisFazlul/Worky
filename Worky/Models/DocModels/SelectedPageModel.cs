﻿using Worky.Project.Documents;

namespace Worky.Models.DocModels
{
    public class SelectedPageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal void SetFromPage(DocumentPage selPage)
        {
            Id = selPage.Id;
            Name = selPage.Name;
        }
    }
}