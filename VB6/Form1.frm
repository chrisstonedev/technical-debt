VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   7350
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   9735
   LinkTopic       =   "Form1"
   ScaleHeight     =   7350
   ScaleWidth      =   9735
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtOJ 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   4800
      MultiLine       =   -1  'True
      TabIndex        =   14
      Top             =   4440
      Width           =   4095
   End
   Begin VB.TextBox txtPJ 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   13
      Top             =   4440
      Width           =   4335
   End
   Begin VB.CommandButton cmdGA 
      Caption         =   "GET alt"
      Height          =   735
      Left            =   7560
      TabIndex        =   8
      Top             =   720
      Width           =   1095
   End
   Begin VB.TextBox txtGX 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   4800
      MultiLine       =   -1  'True
      TabIndex        =   6
      Top             =   2520
      Width           =   4095
   End
   Begin VB.TextBox txtStatus 
      Enabled         =   0   'False
      Height          =   285
      Left            =   7200
      TabIndex        =   5
      Top             =   90
      Width           =   1335
   End
   Begin VB.TextBox txtGJ 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   4
      Top             =   2520
      Width           =   4335
   End
   Begin VB.CommandButton btnPOST 
      Caption         =   "POST"
      Height          =   495
      Left            =   5880
      TabIndex        =   3
      Top             =   1560
      Width           =   2775
   End
   Begin VB.CommandButton btnGM 
      Caption         =   "GET main"
      Height          =   735
      Left            =   5880
      TabIndex        =   2
      Top             =   720
      Width           =   1455
   End
   Begin VB.Frame Frame1 
      Caption         =   "User ID"
      Height          =   495
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   2535
      Begin VB.TextBox TextUserId 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Fixedsys"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C000C0&
         Height          =   285
         Left            =   60
         TabIndex        =   7
         Text            =   "First Name"
         Top             =   180
         Width           =   2415
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Method"
      Height          =   495
      Left            =   2700
      TabIndex        =   1
      Top             =   0
      Width           =   1215
      Begin VB.TextBox txtMethod 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000F&
         BorderStyle     =   0  'None
         BeginProperty Font 
            Name            =   "Fixedsys"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C000C0&
         Height          =   285
         Left            =   60
         TabIndex        =   16
         Text            =   "0"
         Top             =   180
         Width           =   900
      End
   End
   Begin VB.Label Label5 
      Caption         =   "Status Code:"
      Height          =   375
      Left            =   5880
      TabIndex        =   15
      Top             =   120
      Width           =   3015
   End
   Begin VB.Label Label4 
      Caption         =   "POST Output in JSON"
      Height          =   375
      Left            =   4800
      TabIndex        =   12
      Top             =   4200
      Width           =   3015
   End
   Begin VB.Label Label3 
      Caption         =   "GET Output in XML"
      Height          =   375
      Left            =   4800
      TabIndex        =   11
      Top             =   2280
      Width           =   3015
   End
   Begin VB.Label Label2 
      Caption         =   "POST Translate in JSON"
      Height          =   375
      Left            =   120
      TabIndex        =   10
      Top             =   4200
      Width           =   3015
   End
   Begin VB.Label Label1 
      Caption         =   "GET Output in JSON"
      Height          =   375
      Left            =   120
      TabIndex        =   9
      Top             =   2280
      Width           =   3015
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private OurNewMethods As ClassLibrary.NewMethods

Private Sub Form_Initialize()
    Set OurNewMethods = New ClassLibrary.NewMethods
End Sub

Private Sub btnGM_Click()
    Debug.Print OurNewMethods.ReturnFive
    GtSubr
End Sub

Private Sub cmdGA_Click()
    GtSubr TextUserId.Text
End Sub

Private Sub btnPOST_Click()
    Dim httpURL As WinHttp.WinHttpRequest
    Dim UrlString As String
    Dim Output As String

    Set httpURL = New WinHttp.WinHttpRequest
    UrlString = "https://jsonplaceholder.typicode.com/posts"
    httpURL.Open "POST", UrlString, False
    httpURL.SetRequestHeader "Content-type", "application/json"

    Dim XMLDoc As DOMDocument60
    Dim successfulload As Boolean
    Set XMLDoc = New DOMDocument60
    successfulload = XMLDoc.loadXML(txtGX.Text)
    If successfulload Then
        Dim node As IXMLDOMNode
        Dim nodelist As IXMLDOMNodeList
        Set nodelist = XMLDoc.selectNodes("/xml/*")
        Dim jsonText As String
        For Each node In nodelist
            If Len(jsonText) = 0 Then
                jsonText = "{""" + node.baseName + """: """ + node.Text + """"
            Else
                jsonText = jsonText + ",""" + node.baseName + """: """ + node.Text + """"
            End If
        Next node
        If Len(jsonText) > 0 Then
            jsonText = jsonText + "}"
        End If
    Else
        txtGX.Text = XMLDoc.parseError.reason
        txtGX.ForeColor = vbRed
        Exit Sub
    End If

    txtPJ.Text = jsonText
    httpURL.Send jsonText
    Output = httpURL.ResponseText
    txtOJ.Text = Output
    txtStatus.Text = httpURL.Status
    If httpURL.Status <> 201 Then
        txtStatus.ForeColor = vbRed
    Else
        txtStatus.ForeColor = vbBlack
    End If
End Sub

Private Sub GtSubr(Optional ByVal strCFN As String = "")
    Dim objWhr As WinHttp.WinHttpRequest
    Dim strTmp As String
    Dim strOtp As String
    Dim objXml As DOMDocument60
    Dim objXrt As IXMLDOMElement
    Dim objXnd As IXMLDOMNode
    Dim intNTQCL As Integer
    Dim intQCL As Integer
    Dim blnLTWSQ As Boolean
    Dim blnLTWFA As Boolean
    Dim strNOA As String
    Dim strSCh As String
    Dim strCVl As String

    Set objWhr = New WinHttp.WinHttpRequest
    strTmp = "https://jsonplaceholder.typicode.com/todos/1"
    objWhr.Open "GET", strTmp
    objWhr.Send
    strOtp = objWhr.ResponseText
    txtGJ.Text = strOtp
    txtStatus.Text = ""
    If Len(strCFN) > 0 Then
        Set objXml = New DOMDocument60
        Set objXrt = objXml.appendChild(objXml.createElement("Myinfo"))
        objXrt.appendChild(objXml.createElement("FirstName")).Text = "My First Name"
        objXrt.appendChild(objXml.createElement("LastName")).Text = "My Last Name"
        objXrt.appendChild(objXml.createElement("StreetAdd")).Text = "My Address"

        If Len(strCFN) > 0 Then
            Set objXnd = objXml.selectSingleNode("/Myinfo/FirstName")
            objXnd.Text = strCFN
        End If
    Else
        Set objXml = New DOMDocument60
        Set objXrt = objXml.appendChild(objXml.createElement("xml"))
        'Dim positions As IXMLDOMAttribute
        'Set positions = objXml.createAttribute("positions")
        'objXrt.Attributes.setNamedItem positions
        'Dim attributenames As IXMLDOMAttribute
        'Set attributenames = objXml.createAttribute("attributenames")
        'objXrt.Attributes.setNamedItem attributenames
        'Dim allvalues As IXMLDOMAttribute
        'Set allvalues = objXml.createAttribute("allvalues")
        'objXrt.Attributes.setNamedItem allvalues
        Do
            If blnLTWFA Then
                strSCh = ","
            Else
                strSCh = """"
            End If
            intNTQCL = InStr(intQCL + 1, strOtp, strSCh, vbTextCompare)
            If intNTQCL = 0 And blnLTWFA Then
                strSCh = "}"
                intNTQCL = InStr(intQCL + 1, strOtp, strSCh, vbTextCompare)
            End If
            If intNTQCL = 0 Then
                Exit Do
            Else
                'If Len(positions.Text) > 0 Then
                '    positions.Text = positions.Text + "," + CStr(intNTQCL)
                'Else
                '    positions.Text = CStr(intNTQCL)
                'End If

                If blnLTWFA Then
                    strCVl = Replace(Replace(Trim(Mid(strOtp, intQCL + 2, intNTQCL - intQCL - 2)), vbCr, ""), vbLf, "")
                    If Len(strCVl) >= 2 And Left(strCVl, 1) = """" And Right(strCVl, 1) = """" Then
                        strCVl = Mid(strCVl, 2, Len(strCVl) - 2)
                    End If
                    'If Len(allvalues.Text) > 0 Then
                    '    allvalues.Text = allvalues.Text + "," + strCVl
                    'Else
                    '    allvalues.Text = strCVl
                    'End If

                    objXrt.appendChild(objXml.createElement(strNOA)).Text = strCVl

                    blnLTWFA = False
                Else
                    If blnLTWSQ Then
                        strNOA = Mid(strOtp, intQCL + 1, intNTQCL - intQCL - 1)

                        'If Len(attributenames.Text) > 0 Then
                        '    attributenames.Text = attributenames.Text + "," + strNOA
                        'Else
                        '    attributenames.Text = strNOA
                        'End If
                        blnLTWSQ = False
                        blnLTWFA = True
                    Else
                        blnLTWSQ = True
                    End If
                End If
            End If
            intQCL = intNTQCL
        Loop
    End If

    txtGX.Text = objXml.xml
    txtStatus.Text = objWhr.Status
    If objWhr.Status <> 201 Then
        txtStatus.ForeColor = vbRed
    Else
        txtStatus.ForeColor = vbBlack
    End If
End Sub
