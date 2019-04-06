using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{

    public class ManagerSingeton<T>: MonoBehaviour where T:ManagerSingeton<T>  //泛型约束   
         
    {
        //public static T _instance;
        

        //private void Awake()        不在Awake里面调用，而是按需加载   ↓↓
        //{
        //    _instance = this as T;
        //}

        //按需加载
        private static T _instance;
        public static T _Instance {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();   //F12查看到  FindObjectOfType（）方法是有泛型约束的


                    if (_instance == null)
                    {   //如果继续为null，有可能没有挂载脚本，新建一个gameObject
                        new GameObject("Singleton of" + typeof(T)).AddComponent<T>();     //*给new的物体起名
                    }                                                                     //（物体创建了，就会调用Awake方法）
                    else {
                        _instance.Init();
                    }

                    }
                return _instance;

            }
        }

        protected void Awake()    //允许被挂载后，自行创建_instance
        {
            if (_instance == null) {    //如果挂载了脚本，给_instance赋值，如果没挂载，新建一个↑↑
                _instance = this as T;
                Init();     //调用Init（）；

            }
        }

        public virtual void Init() {   //允许子类自己决定是否初始化,方便子类在该方法里做初始化
                                       //决定了Awake初始化的顺序，子类在Init里做初始化，子类的初始化系列操作是在该父类Awake之后的            
        }                              //子类以后不需要使用Awake，父类给自行初始化了                                                 

    }

}
