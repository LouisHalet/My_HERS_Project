using System;
using System.Collections.Generic;
using System.Text;

namespace BackupAndClean {
    public abstract class MyTask {
        
        protected string name;
        protected string taskStatusProtected;
        public string taskStatus
        {
            get { return taskStatusProtected; }
            set { this.taskStatusProtected = value; }
        }
        /// <summary>
        /// Build a task with its name
        /// </summary>
        /// <param name="name"></param>
        public MyTask(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// Start the task execution
        /// </summary>
        /// <returns></returns>
        public abstract Task run();
        /// <summary>
        /// Stop task execution
        /// </summary>
        /// <returns></returns>
        public abstract Task stop();
    }
}