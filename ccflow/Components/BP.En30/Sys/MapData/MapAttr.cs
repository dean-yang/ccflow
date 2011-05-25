using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
   
    /// <summary>
    /// 实体属性
    /// </summary>
    public class MapAttrAttr : EntityMyPKAttr
    {
        /// <summary>
        /// 实体标识
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// 物理表
        /// </summary>
        public const string KeyOfEn = "KeyOfEn";
        /// <summary>
        /// 实体名称
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// 默认值
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// 字段
        /// </summary>
        public const string Field = "Field";
        /// <summary>
        /// 最大长度
        /// </summary>
        public const string MaxLen = "MaxLen";
        /// <summary>
        /// 最小长度
        /// </summary>
        public const string MinLen = "MinLen";
        /// <summary>
        /// 绑定的值
        /// </summary>
        public const string UIBindKey = "UIBindKey";
        /// <summary>
        /// 空件类型
        /// </summary>
        public const string UIContralType = "UIContralType";
        /// <summary>
        /// 宽度
        /// </summary>
        public const string UIWidth = "UIWidth";
        /// <summary>
        /// 是否只读
        /// </summary>
        public const string UIIsEnable = "UIIsEnable";
        /// <summary>
        /// 关联的表的Key
        /// </summary>
        public const string UIRefKey = "UIRefKey";
        /// <summary>
        /// 关联的表的Lab
        /// </summary>
        public const string UIRefKeyText = "UIRefKeyText";
        /// <summary>
        /// 是否可见的
        /// </summary>
        public const string UIVisible = "UIVisible";
        /// <summary>
        /// 是否单独行显示
        /// </summary>
        public const string UIIsLine = "UIIsLine";
        /// <summary>
        /// 序号
        /// </summary>
        public const string IDX = "IDX";
        /// <summary>
        /// 标识（存放临时数据）
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// MyDataType
        /// </summary>
        public const string MyDataType = "MyDataType";
        /// <summary>
        /// 逻辑类型
        /// </summary>
        public const string LGType = "LGType";
        /// <summary>
        /// 编辑类型
        /// </summary>
        public const string EditType = "EditType";
        /// <summary>
        /// 自动填写内容
        /// </summary>
        public const string AutoFullDoc = "AutoFullDoc";
        /// <summary>
        /// 自动填写方式
        /// </summary>
        public const string AutoFullWay = "AutoFullWay";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// 是否是签字
        /// </summary>
        public const string IsSigan = "IsSigan";
        /// <summary>
        /// x
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// y
        /// </summary>
        public const string Y = "Y";
    }
    /// <summary>
    /// 实体属性
    /// </summary>
    public class MapAttr : EntityMyPK
    {
        #region 属性
        public EntitiesNoName HisEntitiesNoName
        {
            get
            {
                if (this.UIBindKey.Contains("."))
                {
                    EntitiesNoName ens = (EntitiesNoName)BP.DA.ClassFactory.GetEns(this.UIBindKey);
                    ens.RetrieveAll();
                    return ens;
                }
                GENoNames myens = new GENoNames(this.UIBindKey, this.Name);
                myens.RetrieveAll();
                return myens;
            }
        }
        public bool IsTableAttr
        {
            get
            {
                return DataType.IsNumStr(this.KeyOfEn.Replace("F", ""));
            }
        }
        public Attr HisAttr
        {
            get
            {
                Attr attr = new Attr();
                attr.Key = this.KeyOfEn;
                attr.Desc = this.Name;
                attr.DefaultVal = this.DefVal;
                attr.Field = this.Field;
                attr.MaxLength = this.MaxLen;
                attr.MinLength = this.MinLen;
                attr.UIBindKey = this.UIBindKey;

                attr.UIIsLine = this.UIIsLine;

                attr.UIHeight = 0;
                if (this.MaxLen > 3000)
                    attr.UIHeight = 10;

                attr.UIWidth = this.UIWidth;
                attr.MyDataType = this.MyDataType;
                attr.UIRefKeyValue = this.UIRefKey;
                attr.UIRefKeyText = this.UIRefKeyText;
                attr.UIVisible = this.UIVisible;
                if (this.IsPK)
                    attr.MyFieldType = FieldType.PK;

                switch (this.LGType)
                {
                    case FieldTypeS.Enum:
                        attr.UIContralType = UIContralType.DDL;
                        attr.MyFieldType = FieldType.Enum;
                        attr.UIDDLShowType = BP.Web.Controls.DDLShowType.SysEnum;
                        attr.UIIsReadonly = this.UIIsEnable;
                        break;
                    case FieldTypeS.FK:
                        attr.UIContralType = UIContralType.DDL;
                        attr.MyFieldType = FieldType.FK;
                        attr.UIDDLShowType = BP.Web.Controls.DDLShowType.Ens;
                        attr.UIRefKeyValue = "No";
                        attr.UIRefKeyText = "Name";
                        attr.UIIsReadonly = this.UIIsEnable;
                        break;
                    default:
                        attr.UIContralType = UIContralType.TB;
                        if (this.IsPK)
                            attr.MyFieldType = FieldType.PK;

                        attr.UIIsReadonly = !this.UIIsEnable;
                        switch (this.MyDataType)
                        {
                            case DataType.AppBoolean:
                                attr.UIContralType = UIContralType.CheckBok;
                                attr.UIIsReadonly = this.UIIsEnable;
                                break;
                            case DataType.AppDate:
                                if (this.Tag == "1")
                                    attr.DefaultVal = DataType.CurrentData;
                                break;
                            case DataType.AppDateTime:
                                if (this.Tag == "1")
                                    attr.DefaultVal = DataType.CurrentData;
                                break;
                            default:
                                break;
                        }
                        break;
                }

                attr.AutoFullWay = this.HisAutoFull;
                attr.AutoFullDoc = this.AutoFullDoc;

                //attr.MyFieldType = FieldType
                //attr.UIDDLShowType= BP.Web.Controls.DDLShowType.Self
                return attr;
            }
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPK
        {
            get
            {
                switch (this.KeyOfEn)
                {
                    case "OID":
                    case "No":
                    case "MyPK":
                        return true;
                    default:
                        return false;
                }
            }
        }
        public EditType HisEditType
        {
            get
            {
                return (EditType)this.GetValIntByKey(MapAttrAttr.EditType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.EditType, (int)value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// AutoFullWay
        /// </summary>
        public AutoFullWay HisAutoFull
        {
            get
            {
                return (AutoFullWay)this.GetValIntByKey(MapAttrAttr.AutoFullWay);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.AutoFullWay, (int)value);
            }
        }
        /// <summary>
        /// 自动填写
        /// </summary>
        public string AutoFullDoc
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.AutoFullDoc);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.AutoFullDoc, value);
            }
        }
        public string KeyOfEn
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.KeyOfEn);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.KeyOfEn, value);
            }
        }
        public FieldTypeS LGType
        {
            get
            {
                return (FieldTypeS)this.GetValIntByKey(MapAttrAttr.LGType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.LGType, (int)value);
            }
        }
        public string LGTypeT
        {
            get
            {
                return this.GetValRefTextByKey(MapAttrAttr.LGType);
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Name
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.Name);
                if (s == "" || s == null)
                    return this.KeyOfEn;
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Name, value);
            }
        }
        public bool IsNum
        {
            get
            {
                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppString:
                    case BP.DA.DataType.AppDate:
                    case BP.DA.DataType.AppDateTime:
                    case BP.DA.DataType.AppBoolean:
                        return false;
                    default:
                        return true;
                }
            }
        }
        public decimal DefValDecimal
        {
            get
            {
                return decimal.Parse(this.DefVal);
            }
        }
        public string DefValReal
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.DefVal);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefVal
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.DefVal);
                if (this.IsNum)
                {
                    if (s == "")
                        return "0";
                }

                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppDate:
                        if (this.Tag == "1" || s == "@RDT")
                            return DataType.CurrentData;
                        else
                            return "          ";
                        break;
                    case BP.DA.DataType.AppDateTime:
                        if (this.Tag == "1" || s == "@RDT")
                            return DataType.CurrentDataTime;
                        else
                            return "               ";
                        //return "    -  -    :  ";
                        break;
                    default:
                        break;
                }

                if (s.Contains("@") == false)
                    return s;

                switch (s.ToLower())
                {
                    case "@webuser.no":
                        return BP.Web.WebUser.No;
                        break;
                    case "@webuser.name":
                        return BP.Web.WebUser.Name;
                        break;
                    case "@webuser.fk_dept":
                        return BP.Web.WebUser.FK_Dept;
                    case "@webuser.fk_deptname":
                        return BP.Web.WebUser.FK_DeptName;
                    case "@webuser.fk_station":
                        return BP.Web.WebUser.FK_Station;
                    case "@fk_ny":
                        return DataType.CurrentYearMonth;
                    case "@fk_nd":
                        return DataType.CurrentYear;
                    case "@fk_yf":
                        return DataType.CurrentMonth;
                    case "@rdt":
                        return DataType.CurrentDataTime;
                    case "@rd":
                        return DataType.CurrentData;
                    case "@yyyy年mm月dd日":
                        return DataType.CurrentDataCNOfLong;
                    case "@yy年mm月dd日":
                        return DataType.CurrentDataCNOfShort;
                    default:
                        return s;
                    //throw new Exception("没有约定的变量默认值类型" + s);
                }
                return this.GetValStrByKey(MapAttrAttr.DefVal);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        public bool DefValOfBool
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.DefVal, false);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        /// <summary>
        /// 字段
        /// </summary>
        public string Field
        {
            get
            {
                return this.KeyOfEn;
                //string s= this.GetValStrByKey(MapAttrAttr.Field);
                //if (s == null || s == "" || s.Trim()=="" )
                //    return this.Key;
                //else
                //    return s;
            }
        }
        public BP.Web.Controls.TBType HisTBType
        {
            get
            {
                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppRate:
                    case BP.DA.DataType.AppMoney:
                        return BP.Web.Controls.TBType.Moneny;
                    case BP.DA.DataType.AppInt:
                    case BP.DA.DataType.AppFloat:
                    case BP.DA.DataType.AppDouble:
                        return BP.Web.Controls.TBType.Num;
                    default:
                        return BP.Web.Controls.TBType.TB;
                }
            }
        }
        public int MyDataType
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.MyDataType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MyDataType, value);
            }
        }
        public string MyDataTypeS
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        return "String";
                    case DataType.AppInt:
                        return "Int";
                    case DataType.AppFloat:
                        return "Float";
                    case DataType.AppMoney:
                        return "Money";
                    case DataType.AppDate:
                        return "Date";
                    case DataType.AppDateTime:
                        return "DateTime";
                    case DataType.AppBoolean:
                        return "Bool";
                    default:
                        throw new Exception("sdsdsd");
                }
            }
            set
            {

                switch (value)
                {
                    case "String":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppString);
                        break;
                    case "Int":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppInt);
                        break;
                    case "Float":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppFloat);
                        break;
                    case "Money":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppMoney);
                        break;
                    case "Date":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppDate);
                        break;
                    case "DateTime":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppDateTime);
                        break;
                    case "Bool":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppBoolean);
                        break;
                    default:
                        throw new Exception("sdsdsd");
                }

            }
        }
        public string MyDataTypeStr
        {
            get
            {
                return DataType.GetDataTypeDese(this.MyDataType);
            }
        }
        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLen
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppDate:
                        return 10;
                    case DataType.AppDateTime:
                        return 20;
                    default:
                        break;
                }

                int i = this.GetValIntByKey(MapAttrAttr.MaxLen);
                if (i > 4000)
                    i = 400;
                return i;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MaxLen, value);
            }
        }
        /// <summary>
        /// 最小长度
        /// </summary>
        public int MinLen
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.MinLen);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MinLen, value);
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.GroupID, value);
            }
        }
        public bool IsBigDoc
        {
            get
            {
                if (this.MaxLen > 3000)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public int UIWidth
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        return this.GetValIntByKey(MapAttrAttr.UIWidth);
                    case DataType.AppFloat:
                    case DataType.AppInt:
                    case DataType.AppMoney:
                    case DataType.AppRate:
                    case DataType.AppDouble:
                        return 80;
                    case DataType.AppDate:
                        return 70;
                    case DataType.AppDateTime:
                        return 100;
                    default:
                        return 70;
                }
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIWidth, value);
            }
        }
        public int UIWidthOfLab
        {
            get
            {
                return 0;

                //Graphics2D g2 = (Graphics2D)g;
                //g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
                //                        RenderingHints.VALUE_ANTIALIAS_ON);

                //int textWidth = getFontMetrics(g2.getFont()).bytesWidth(str.getBytes(), 0, str.getBytes().length); 

            }
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool UIVisible
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIVisible);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIVisible, value);
            }
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool UIIsEnable
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsEnable);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsEnable, value);
            }
        }
        /// <summary>
        /// 是否单独行显示
        /// </summary>
        public bool UIIsLine
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsLine);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsLine, value);
            }
        }
        public bool IsSigan
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.IsSigan);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.IsSigan, value);
            }
        }
        /// <summary>
        /// 绑定的值
        /// </summary>
        public string UIBindKey
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.UIBindKey);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIBindKey, value);
            }
        }
        /// <summary>
        /// 关联的表的Key
        /// </summary>
        public string UIRefKey
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.UIRefKey);
                if (s == "" || s == null)
                    s = "No";
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIRefKey, value);
            }
        }
        /// <summary>
        /// 关联的表的Lab
        /// </summary>
        public string UIRefKeyText
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.UIRefKeyText);
                if (s == "" || s == null)
                    s = "Name";
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIRefKeyText, value);
            }
        }
        /// <summary>
        /// 标识
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.Tag);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Tag, value);
            }
        }
        /// <summary>
        /// 控件类型
        /// </summary>
        public UIContralType UIContralType
        {
            get
            {
                return (UIContralType)this.GetValIntByKey(MapAttrAttr.UIContralType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIContralType, (int)value);
            }
        }

        public string UIContralTypeT
        {
            get
            {
                return this.GetValRefTextByKey(MapAttrAttr.UIContralType);
            }
        }
        public string F_Desc
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        if (this.UIVisible == false)
                            return "长度" + this.MinLen + "-" + this.MaxLen + "不可见";
                        else
                            return "长度" + this.MinLen + "-" + this.MaxLen;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                    case DataType.AppInt:
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                        if (this.UIVisible == false)
                            return "不可见";
                        else
                            return "";
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int IDX
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.IDX);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.IDX, value);
            }
        }
        public int X
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.X);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.X, value);
            }
        }
        public int Y
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.Y);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Y, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 实体属性
        /// </summary>
        public MapAttr()
        {
        }
        public MapAttr(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        public MapAttr(string fk_mapdata, string key)
        {
            this.FK_MapData = fk_mapdata;
            this.KeyOfEn = key;
            this.Retrieve(MapAttrAttr.FK_MapData, this.FK_MapData, MapAttrAttr.KeyOfEn, this.KeyOfEn);
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_MapAttr");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "实体属性";
                map.EnType = EnType.Sys;

                map.AddMyPK();

                map.AddTBString(MapAttrAttr.FK_MapData, null, "实体标识", true, true, 1, 30, 20);
                map.AddTBString(MapAttrAttr.KeyOfEn, null, "属性", true, true, 1, 30, 20);

                map.AddTBString(MapAttrAttr.Name, null, "描述", true, false, 0, 100, 20);
                map.AddTBString(MapAttrAttr.DefVal, null, "默认值", false, false, 0, 30, 20);

                map.AddDDLSysEnum(MapAttrAttr.UIContralType, 0, "空件类型", true, false, MapAttrAttr.UIContralType, "@0=文本框@1=下拉框");
                map.AddDDLSysEnum(MapAttrAttr.MyDataType, 0, "数据类型", true, false, MapAttrAttr.MyDataType,
                    "@1=文本(String)@2=整型(Int)@3=浮点(Float)@4=布尔@5=Double@6=AppDate@7=AppDateTime@8=AppMoney@9=AppRate");

                map.AddDDLSysEnum(MapAttrAttr.LGType, 0, "逻辑类型", true, false, MapAttrAttr.LGType, "@0=普通@1=枚举@2=外键");

                map.AddTBInt(MapAttrAttr.UIWidth, 100, "宽度", true, false);
                map.AddTBInt(MapAttrAttr.MinLen, 0, "最小长度", true, false);
                map.AddTBInt(MapAttrAttr.MaxLen, 500, "最大长度", true, false);

                map.AddTBString(MapAttrAttr.UIBindKey, null, "绑定的信息", true, false, 0, 100, 20);
                map.AddTBString(MapAttrAttr.UIRefKey, null, "绑定的Key", true, false, 0, 30, 20);
                map.AddTBString(MapAttrAttr.UIRefKeyText, null, "绑定的Text", true, false, 0, 30, 20);

                map.AddBoolean(MapAttrAttr.UIVisible, true, "是否可见", true, true);
                map.AddBoolean(MapAttrAttr.UIIsEnable, false, "是否只读", true, true);
                map.AddBoolean(MapAttrAttr.UIIsLine, false, "是否单独栏显示", true, true);

                map.AddTBString(MapAttrAttr.Tag, null, "标识（存放临时数据）", true, false, 0, 100, 20);
                map.AddTBInt(MapAttrAttr.EditType, 0, "编辑类型", true, false);

                map.AddTBString(MapAttrAttr.AutoFullDoc, null, "自动填写内容", false, false, 0, 500, 20);
                map.AddDDLSysEnum(MapAttrAttr.AutoFullWay, 0, "自动填写方式", true, false, MapAttrAttr.AutoFullWay,
                    "@0=不设置@1=本表单中数据计算@2=利用SQL自动填充@3=本表单中外键列@4=对明细表的列求值");

                map.AddTBInt(MapAttrAttr.IDX, 0, "序号", true, false);
                map.AddTBInt(MapAttrAttr.GroupID, 0, "GroupID", true, false);

                // 是否是签字，操作员字段有效。2010-09-23 增加。
                map.AddTBInt(MapAttrAttr.IsSigan, 0, "签字？", true, false);

                map.AddTBInt(MapAttrAttr.X, 0, "X", true, false);
                map.AddTBInt(MapAttrAttr.Y, 0, "Y", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// 检查是否复合要求
        /// </summary>
        /// <returns></returns>
        public string DoCheckFullWay()
        {
            string doc = "";
            switch (this.HisAutoFull)
            {
                case AutoFullWay.Way0: //不处理。
                    break;
                case AutoFullWay.Way1_JS: //JS 方式处理。
                    break;
                case AutoFullWay.Way2_SQL:
                    break;
                case AutoFullWay.Way3_FK:
                    break;
                case AutoFullWay.Way4_Dtl:
                    break;
                default:
                    break;
            }
            return "检查成功！！！";
        }
        public void DoUp()
        {
            this.DoOrderUp(MapAttrAttr.GroupID, this.GroupID.ToString(), MapAttrAttr.IDX);
        }
        public void DoDown()
        {
            this.DoOrderDown(MapAttrAttr.GroupID, this.GroupID.ToString(), MapAttrAttr.IDX);
        }
        public void DoJump(MapAttr attrTo)
        {
            if (attrTo.IDX <= this.IDX)
                this.DoJumpUp(attrTo);
            else
                this.DoJumpDown(attrTo);
        }
        private string DoJumpUp(MapAttr attrTo)
        {
            string sql = "UPDATE Sys_MapAttr SET IDX=IDX+1 WHERE IDX <=" + attrTo.IDX + " AND FK_MapData='" + this.FK_MapData + "' AND GroupID="+this.GroupID;
            DBAccess.RunSQL(sql);
            this.IDX = attrTo.IDX - 1;
            this.GroupID = attrTo.GroupID;
            this.Update();
            return null;
        }
        private string DoJumpDown(MapAttr attrTo)
        {
            string sql = "UPDATE Sys_MapAttr SET IDX=IDX-1 WHERE IDX <=" + attrTo.IDX + " AND FK_MapData='" + this.FK_MapData + "' AND GroupID=" + this.GroupID;
            DBAccess.RunSQL(sql);
            this.IDX = attrTo.IDX + 1;
            this.GroupID = attrTo.GroupID;
            this.Update();
            return null;
        }
        protected override bool beforeUpdate()
        {
            switch (this.MyDataType)
            {
                case DataType.AppDateTime:
                    this.MaxLen = 20;
                    break;
                case DataType.AppDate:
                    this.MaxLen = 10;
                    break;
                default:
                    break;
            }
            this.MyPK = this.FK_MapData + "_" + this.KeyOfEn;
            return base.beforeUpdate();
        }
        protected override bool beforeDelete()
        {
            try
            {
                BP.DA.DBAccess.RunSQL("alter table " + this.FK_MapData + " drop column " + this.KeyOfEn);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
        protected override bool beforeInsert()
        {
            if (this.KeyOfEn == null || this.KeyOfEn.Trim() == "")
            {
                try
                {
                    this.KeyOfEn = BP.DA.chs2py.convert(this.Name);
                    if (this.KeyOfEn.Length > 20)
                        this.KeyOfEn = BP.DA.chs2py.ConvertWordFirst(this.Name);

                    if (this.KeyOfEn == null || this.KeyOfEn.Trim() == "")
                        throw new Exception("@请输入字段描述或者字段名称。");
                }
                catch (Exception ex)
                {
                    throw new Exception("@请输入字段描述或字段名称。异常信息:" + ex.Message);
                }
            }
            else
            {
                if (this.KeyOfEn.Contains("_") == false)
                {
                    System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
                    if (reg1.IsMatch(this.KeyOfEn) == false)
                    {
                        throw new Exception("您输入的字段英文名[" + this.KeyOfEn + "]不符合要求，请按英文、拼音缩写填写或者由系统写完成。");
                    }
                }
            }

            string keyofenC = this.KeyOfEn.Clone() as string;
            keyofenC = keyofenC.ToLower();

            if (PubClass.KeyFields.Contains("," + keyofenC + ",") == true)
                throw new Exception("@错误:[" + this.KeyOfEn + "]是字段关键字，您不能用它做字段。");

            if (this.IsExit(BP.Sys.MapAttrAttr.KeyOfEn, this.KeyOfEn,
                BP.Sys.MapAttrAttr.FK_MapData, this.FK_MapData))
            {
                throw new Exception("@已经存在字段名称(" + this.Name + ")字段(" + this.KeyOfEn + ")");
            }

            //int i = 0;
            //while ()
            //{
            //    this.KeyOfEn = this.KeyOfEn + i;
            //    i++;
            //}

            this.IDX = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM Sys_MapAttr WHERE FK_MapData='" + this.FK_MapData + "'") + 1;
            this.MyPK = this.FK_MapData + "_" + this.KeyOfEn;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// 实体属性s
    /// </summary>
    public class MapAttrs : EntitiesMyPK
    {
        #region 构造
        /// <summary>
        /// 实体属性s
        /// </summary>
        public MapAttrs()
        {
        }
        /// <summary>
        /// 实体属性s
        /// </summary>
        public MapAttrs(string fk_map)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MapAttrAttr.FK_MapData, fk_map);
            qo.addOrderBy(MapAttrAttr.GroupID,MapAttrAttr.IDX);
            qo.DoQuery();
        }
        public int SearchMapAttrsYesVisable(string fk_map)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MapAttrAttr.FK_MapData, fk_map);
            qo.addAnd();
            qo.AddWhere(MapAttrAttr.UIVisible, 1);
            qo.addOrderBy(MapAttrAttr.GroupID, MapAttrAttr.IDX);
           // qo.addOrderBy(MapAttrAttr.IDX);
            return qo.DoQuery();

        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapAttr();
            }
        }
        public int WithOfCtl
        {
            get
            {
                int i = 0;
                foreach (MapAttr item in this)
                {
                    if (item.UIVisible == false)
                        continue;

                    i += item.UIWidth;
                }
                return i;
            }
        }
        #endregion
    }
}
