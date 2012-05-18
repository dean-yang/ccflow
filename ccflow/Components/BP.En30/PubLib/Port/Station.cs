using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Port
{	 
	/// <summary>
	/// 岗位属性
	/// </summary>
    public class StationAttr : EntityNoNameAttr
    {
        public const string IsDtl = "IsDtl";
        public const string StaGrade = "StaGrade";
    }
	/// <summary>
	/// 岗位
	/// </summary>
    public class Station : EntityNoName
    {
        #region 实现基本的方方法
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));

                return this.GetValStrByKey("Name");
            }
        }
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
        public int StaGrade
        {
            get
            {
                return this.GetValIntByKey(StationAttr.StaGrade);
            }
        }


        public bool IsDtl
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 岗位
        /// </summary> 
        public Station()
        {
        }
        /// <summary>
        /// 岗位
        /// </summary>
        /// <param name="_No"></param>
        public Station(string _No) : base(_No) { }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_Station");
                map.EnDesc = this.ToE("Station", "岗位"); // "岗位";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.CodeStruct = "2222222"; // 最大级别是 7 .
                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(EmpAttr.No, null, this.ToE("No", "编号"), true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 100, 100);
                map.AddDDLSysEnum(StationAttr.StaGrade, 0, "类型", true, false, StationAttr.StaGrade, "@1=高层岗@2=中层岗@3=执行岗");

                //switch (BP.SystemConfig.SysNo)
                //{
                //    case BP.SysNoList.WF:
                //        map.AddDDLSysEnum(StationAttr.StaGrade, 0, "类型", true, false, StationAttr.StaGrade, "@1=高层岗@2=中层岗@3=执行岗");
                //        break;
                //    default:
                //        break;
                //}

                // map.AddTBInt(DeptAttr.Grade, 0, "级次", true, true);
                //map.AddBoolean(DeptAttr.IsDtl, true, "是否明细", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	 /// <summary>
	 /// 岗位s
	 /// </summary>
	public class Stations : EntitiesNoName
	{
		/// <summary>
		/// 岗位
		/// </summary>
        public Stations() { }
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Station();
			}
		}
	}
}
