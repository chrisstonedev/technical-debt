VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsOrder"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private m_strLineBuffer As String * 52

Public Property Get Customer() As String
    Customer = Mid(m_strLineBuffer, 1, 10)
End Property

Public Property Let Customer(ByVal strCustomer As String)
    m_strLineBuffer = Left(strCustomer & Space(10), 10) & Mid(m_strLineBuffer, 11)
End Property

Public Property Get DateOrdered() As String
    DateOrdered = Mid(m_strLineBuffer, 11, 19)
End Property

Public Property Let DateOrdered(ByVal strDateOrdered As String)
    m_strLineBuffer = Left(m_strLineBuffer, 10) & Left(strDateOrdered & Space(19), 19) & Mid(m_strLineBuffer, 30)
End Property

Public Property Get Price() As Double
    Price = Mid(m_strLineBuffer, 30, 8)
End Property

Public Property Let Price(ByVal dblPrice As Double)
    m_strLineBuffer = Left(m_strLineBuffer, 29) & Left(CStr(dblPrice) & Space(8), 8) & Mid(m_strLineBuffer, 38)
End Property

Public Property Get Product() As String
    Product = Mid(m_strLineBuffer, 38, 10)
End Property

Public Property Let Product(ByVal strProduct As String)
    m_strLineBuffer = Left(m_strLineBuffer, 37) & Left(strProduct & Space(10), 10) & Mid(m_strLineBuffer, 48)
End Property

Public Property Get Quantity() As Integer
    Quantity = Mid(m_strLineBuffer, 48, 5)
End Property

Public Property Let Quantity(ByVal intQuantity As Integer)
    m_strLineBuffer = Left(m_strLineBuffer, 47) & Left(CStr(intQuantity) & Space(5), 5) & Mid(m_strLineBuffer, 53)
End Property

Private Sub Class_Initialize()
    m_strLineBuffer = ""
    DateOrdered = Format(Now(), "yyyy-mm-dd hh:nn:ss")
End Sub

Public Sub FromDatabaseFormat(ByVal strLineBuffer As String)
    m_strLineBuffer = strLineBuffer
End Sub

Public Function ToDatabaseFormat() As String
    ToDatabaseFormat = m_strLineBuffer
End Function
