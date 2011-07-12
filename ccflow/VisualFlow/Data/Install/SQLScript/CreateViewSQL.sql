/****** 对象:  View WF_EmpWorks    脚本日期: 03/12/2011 21:42:50 ******/
@GO
/*  WF_EmpWorks  */
@GO
CREATE VIEW WF_EmpWorks
AS
SELECT     A.WorkID, A.Rec AS Starter, A.FK_Flow, C.FlowName,
C.NodeID, C.Name AS NodeName, A.Title, A.RDT, B.RDT AS ADT, 
B.SDT, B.FK_Emp,B.FID ,C.FK_FlowSort
FROM  WF_GenerWorkFlow A, WF_GenerWorkerList B, WF_Node C
WHERE     (B.IsEnable = 1) AND (B.IsPass = 0)
 AND A.WorkID = B.WorkID AND A.FK_Node = B.FK_Node AND B.FK_Node = C.NodeID
@GO

/*  WF_GenerEmpWorks  */
@GO
CREATE VIEW WF_GenerEmpWorks
AS
SELECT FK_Flow, FK_Emp, COUNT(*) AS NUM  FROM WF_GenerWorkerList
 WHERE IsEnable=1 AND IsPass=0 GROUP BY FK_FLOW, FK_Emp
@GO


/*  WF_NodeExt  */
@GO
CREATE VIEW WF_NodeExt
AS
SELECT NODEID AS NO , NAME AS NAME FROM WF_Node
@GO
 
