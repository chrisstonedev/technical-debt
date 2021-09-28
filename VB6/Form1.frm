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
      TabIndex        =   20
      Top             =   4440
      Width           =   4095
   End
   Begin VB.TextBox txtPJ 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   19
      Top             =   4440
      Width           =   4335
   End
   Begin VB.CommandButton cmdGA 
      Caption         =   "GET alt"
      Height          =   735
      Left            =   7560
      TabIndex        =   14
      Top             =   720
      Width           =   1095
   End
   Begin VB.TextBox txtGX 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   4800
      MultiLine       =   -1  'True
      TabIndex        =   12
      Top             =   2520
      Width           =   4095
   End
   Begin VB.TextBox txtStatus 
      Enabled         =   0   'False
      Height          =   285
      Left            =   7200
      TabIndex        =   11
      Top             =   90
      Width           =   1335
   End
   Begin VB.TextBox txtGJ 
      Enabled         =   0   'False
      Height          =   1455
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   10
      Top             =   2520
      Width           =   4335
   End
   Begin VB.CommandButton btnPOST 
      Caption         =   "POST"
      Height          =   495
      Left            =   5880
      TabIndex        =   9
      Top             =   1560
      Width           =   2775
   End
   Begin VB.CommandButton btnGM 
      Caption         =   "GET main"
      Height          =   735
      Left            =   5880
      TabIndex        =   8
      Top             =   720
      Width           =   1455
   End
   Begin VB.Frame Frame1 
      Caption         =   "User ID"
      Height          =   495
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   1215
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
         ForeColor       =   &H00C00000&
         Height          =   285
         Left            =   60
         TabIndex        =   13
         Text            =   "Text1"
         Top             =   180
         Width           =   1095
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Frame2"
      Height          =   495
      Left            =   1350
      TabIndex        =   1
      Top             =   0
      Width           =   1215
   End
   Begin VB.Frame Frame3 
      Caption         =   "Frame3"
      Height          =   495
      Left            =   2700
      TabIndex        =   2
      Top             =   0
      Width           =   1215
   End
   Begin VB.Frame Frame4 
      Caption         =   "Frame4"
      Height          =   495
      Left            =   4050
      TabIndex        =   3
      Top             =   0
      Width           =   1215
   End
   Begin VB.Frame Frame5 
      Caption         =   "Frame5"
      Height          =   495
      Left            =   0
      TabIndex        =   4
      Top             =   600
      Width           =   1215
   End
   Begin VB.Frame Frame6 
      Caption         =   "Frame6"
      Height          =   495
      Left            =   1350
      TabIndex        =   5
      Top             =   600
      Width           =   1215
   End
   Begin VB.Frame Frame7 
      Caption         =   "Frame7"
      Height          =   495
      Left            =   2700
      TabIndex        =   6
      Top             =   600
      Width           =   1215
   End
   Begin VB.Frame Frame8 
      Caption         =   "Frame8"
      Height          =   495
      Left            =   4050
      TabIndex        =   7
      Top             =   600
      Width           =   1215
   End
   Begin VB.Label Label5 
      Caption         =   "Status Code:"
      Height          =   375
      Left            =   5880
      TabIndex        =   21
      Top             =   120
      Width           =   3015
   End
   Begin VB.Label Label4 
      Caption         =   "POST Output in JSON"
      Height          =   375
      Left            =   4800
      TabIndex        =   18
      Top             =   4200
      Width           =   3015
   End
   Begin VB.Label Label3 
      Caption         =   "GET Output in XML"
      Height          =   375
      Left            =   4800
      TabIndex        =   17
      Top             =   2280
      Width           =   3015
   End
   Begin VB.Label Label2 
      Caption         =   "POST Translate in JSON"
      Height          =   375
      Left            =   120
      TabIndex        =   16
      Top             =   4200
      Width           =   3015
   End
   Begin VB.Label Label1 
      Caption         =   "GET Output in JSON"
      Height          =   375
      Left            =   120
      TabIndex        =   15
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

Private Sub btnGM_Click()
    LongMethod
End Sub

Private Sub cmdGA_Click()
    LongMethod TextUserId.Text
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

Private Sub LongMethod(Optional ByVal Customfirstname As String = "")
    'architecture debt
    'build debt
    'requirement debt
    'code debt
    'documentation debt
    'technology debt
    'test debt

    Dim httpURL As WinHttp.WinHttpRequest
    Dim UrlString As String
    Dim Output As String
    Dim XmlContent As DOMDocument60
    Dim XMLRoot As IXMLDOMElement

    Set httpURL = New WinHttp.WinHttpRequest
    UrlString = "https://jsonplaceholder.typicode.com/todos/1"
    httpURL.Open "GET", UrlString
    httpURL.Send
    Output = httpURL.ResponseText
    txtGJ.Text = Output
    txtStatus.Text = ""
    If Len(Customfirstname) > 0 Then
        Set XmlContent = New DOMDocument60
        Set XMLRoot = XmlContent.appendChild(XmlContent.createElement("Myinfo"))
        XMLRoot.appendChild(XmlContent.createElement("FirstName")).Text = "My First Name"
        XMLRoot.appendChild(XmlContent.createElement("LastName")).Text = "My Last Name"
        XMLRoot.appendChild(XmlContent.createElement("StreetAdd")).Text = "My Address"

        If Len(Customfirstname) > 0 Then
            Dim node As IXMLDOMNode
            Set node = XmlContent.selectSingleNode("/Myinfo/FirstName")
            node.Text = Customfirstname
        End If
    Else
        Set XmlContent = New DOMDocument60
        Set XMLRoot = XmlContent.appendChild(XmlContent.createElement("xml"))
        'Dim positions As IXMLDOMAttribute
        'Set positions = XmlContent.createAttribute("positions")
        'XMLRoot.Attributes.setNamedItem positions
        'Dim attributenames As IXMLDOMAttribute
        'Set attributenames = XmlContent.createAttribute("attributenames")
        'XMLRoot.Attributes.setNamedItem attributenames
        'Dim allvalues As IXMLDOMAttribute
        'Set allvalues = XmlContent.createAttribute("allvalues")
        'XMLRoot.Attributes.setNamedItem allvalues
        Dim newtempquotecharloc As Integer
        Dim quotecharloc As Integer
        Dim lasttripwasthestartquote As Boolean
        Dim lasttripwasthefullattribute As Boolean
        Dim nameofattribute As String
        Do
            Dim searchcharacter As String
            If lasttripwasthefullattribute Then
                searchcharacter = ","
            Else
                searchcharacter = """"
            End If
            newtempquotecharloc = InStr(quotecharloc + 1, Output, searchcharacter, vbTextCompare)
            If newtempquotecharloc = 0 And lasttripwasthefullattribute Then
                searchcharacter = "}"
                newtempquotecharloc = InStr(quotecharloc + 1, Output, searchcharacter, vbTextCompare)
            End If
            If newtempquotecharloc = 0 Then
                Exit Do
            Else
                'If Len(positions.Text) > 0 Then
                '    positions.Text = positions.Text + "," + CStr(newtempquotecharloc)
                'Else
                '    positions.Text = CStr(newtempquotecharloc)
                'End If

                If lasttripwasthefullattribute Then
                    Dim currentvalue As String
                    currentvalue = Replace(Replace(Trim(Mid(Output, quotecharloc + 2, newtempquotecharloc - quotecharloc - 2)), vbCr, ""), vbLf, "")
                    If Len(currentvalue) >= 2 And Left(currentvalue, 1) = """" And Right(currentvalue, 1) = """" Then
                        currentvalue = Mid(currentvalue, 2, Len(currentvalue) - 2)
                    End If
                    'If Len(allvalues.Text) > 0 Then
                    '    allvalues.Text = allvalues.Text + "," + currentvalue
                    'Else
                    '    allvalues.Text = currentvalue
                    'End If

                    XMLRoot.appendChild(XmlContent.createElement(nameofattribute)).Text = currentvalue

                    lasttripwasthefullattribute = False
                Else
                    If lasttripwasthestartquote Then
                        nameofattribute = Mid(Output, quotecharloc + 1, newtempquotecharloc - quotecharloc - 1)

                        'If Len(attributenames.Text) > 0 Then
                        '    attributenames.Text = attributenames.Text + "," + nameofattribute
                        'Else
                        '    attributenames.Text = nameofattribute
                        'End If
                        lasttripwasthestartquote = False
                        lasttripwasthefullattribute = True
                    Else
                        lasttripwasthestartquote = True
                    End If
                End If
            End If
            quotecharloc = newtempquotecharloc
        Loop
    End If

    txtGX.Text = XmlContent.xml
    txtStatus.Text = httpURL.Status
    If httpURL.Status <> 201 Then
        txtStatus.ForeColor = vbRed
    Else
        txtStatus.ForeColor = vbBlack
    End If
End Sub

