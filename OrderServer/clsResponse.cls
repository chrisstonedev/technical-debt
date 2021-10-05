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

Private m_strFieldID As String
Private m_strFieldName As String
Private m_strUserResponse As String

Public Property Get FieldID() As String
    FieldID = m_strFieldID
End Property

Public Property Let FieldID(ByVal strFieldID As String)
    m_strFieldID = strFieldID
End Property

Public Property Get FieldName() As String
    FieldName = m_strFieldName
End Property

Public Property Let FieldName(ByVal strFieldName As String)
    m_strFieldName = strFieldName
End Property

Public Property Get UserResponse() As String
    UserResponse = m_strUserResponse
End Property

Public Property Let UserResponse(ByVal strUserResponse As String)
    m_strUserResponse = strUserResponse
End Property

Private Sub Class_Initialize()
    m_strFieldID = ""
    m_strFieldName = "Field name not found"
    m_strUserResponse = ""
End Sub

