VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsResponse"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private m_strFID As String
Private m_strFN As String
Private m_strRES As String

Public Property Get FID() As String
    FID = m_strFID
End Property

Public Property Let FID(ByVal strFID As String)
    m_strFID = strFID
End Property

Public Property Get FN() As String
    FN = m_strFN
End Property

Public Property Let FN(ByVal strFN As String)
    m_strFN = strFN
End Property

Public Property Get RES() As String
    RES = m_strRES
End Property

Public Property Let RES(ByVal strRES As String)
    m_strRES = strRES
End Property

Private Sub Class_Initialize()
    m_strFID = ""
    m_strFN = "Field name not found"
    m_strRES = ""
End Sub

