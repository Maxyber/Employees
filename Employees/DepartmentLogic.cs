﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Employees
{
    public class DepartmentLogic
    {
        public string path; // Путь к базе данных подразделений компании
        int count; // Общее количество подразделений в компании
        ObservableCollection<Department> depList; // Список всех подразделений компании
        int nextId; // Следующий ID, который необходимо присвоить подразделению

        public DepartmentLogic(string vPath)
        {
            path = vPath;
            nextId = 1;
            depList = new ObservableCollection<Department>();
        }
        /// <summary>
        /// Возвращает список подразделений в компании
        /// </summary>
        public ObservableCollection<Department> DepList
        {
            get { return depList; }
        }
        /// <summary>
        /// Возвращает количество подразделений в компании
        /// </summary>
        public int Count
        {
            get { return count; }
        }
        /// <summary>
        /// Возвращает ID для присвоения новому подразделению
        /// </summary>
        public int NextID
        {
            get { return nextId; }
        }
        /// <summary>
        /// Загружает информацию о подразделениях из базы данных
        /// </summary>
        void LoadData()
        {

        }
        /// <summary>
        /// Сохраняет список подразделений в базу данных
        /// </summary>
        void SaveData()
        {

        }
        /// <summary>
        /// Добавляет новое подразделение в список
        /// </summary>
        public void AddDepartment(Department dep)
        {
            this.depList.Add(dep);
            count++;
            nextId++;
            SaveData();
        }
        /// <summary>
        /// Удаляет подразделение из списка
        /// </summary>
        public void RemoveDepartment(Department dep)
        {
            this.depList.Remove(dep);
            count--;
            SaveData();
        }
        public ObservableCollection<string> ToList()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            for (int i = 0; i < count; i++)
                result.Add(depList[i].Name);
            return result;
        }
    }
}
