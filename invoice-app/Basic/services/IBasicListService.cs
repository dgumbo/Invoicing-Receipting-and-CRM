
//using invoice_app;
//using invoice_app.Basic.models;
//using invoice_demo_app.Basic.models;
//using invoice_demo_app.Basic.services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using System;
//using System.Collections.Generic;
///**
//*
//* @author dgumbo
//* @param <T>
//* @param <B>
//*/
//namespace invoice_demo_app.Basic.services
//{
//    public interface IBasicListService<T, B> : IBasicService<T> where T : ItemWithList<B> where B : BaseEntity
//    {
//        public new T ValidateRequiredBaseEntityProperties(T entityBeforePersist)
//        {
//            DateTime currentTime = DateTime.Now;
//            if (entityBeforePersist.CreatedByUser == null) { entityBeforePersist.CreatedByUser = "dgumbo"; }
//            if (entityBeforePersist.CreationTime != null && entityBeforePersist.CreationTime.Year < 1950) { entityBeforePersist.CreationTime = currentTime; }

//            entityBeforePersist.ModificationTime = currentTime;
//            entityBeforePersist.ModifiedByUser = "dgumbo";

//            List<B> listItems = entityBeforePersist.getListItems();
//            foreach (B item in listItems)
//            {
//                if (item.CreatedByUser == null) { item.CreatedByUser = "dgumbo"; }
//                if (item.CreationTime != null && item.CreationTime.Year < 1950) { item.CreationTime = currentTime; }

//                item.ModificationTime = currentTime;
//                item.ModifiedByUser = "dgumbo";
//            }

//            return entityBeforePersist;
//        }
//    }
//}