using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
public class spell : MonoBehaviour
{
	public string spell_being_done="none";
	public TextAsset textfile;
	private DollarRecognizer reco = new DollarRecognizer();
	//public static SerialPort sPort=new SerialPort("COM5",9600);
	string data_copy="";
	List<Vector2> c_list = new List<Vector2>();
	TcpListener server=null;
	Byte[] bytes=new Byte[256];
	StringBuilder data=new StringBuilder();
	TcpClient wand=null;
	NetworkStream stream=null;
	int n_bytes;
    // Start is called before the first frame update
    void Start()
    {
		//sPort.Open();
		//sPort.ReadTimeout=100;
        string t=textfile.text;
		char[] sp={'@'};
		char[] sp1={'*'};
		char[] sp2={','};
		IEnumerable t_list=t.Split(sp);
		sp[0]='#';
		foreach (string s in t_list)
		{
			if (s!=null)
			{
				string[] temp=s.Split(sp);
				IEnumerable coord_list_s=temp[1].Split(sp1);
				List<Vector2> coord_list=new List<Vector2>();
				foreach (string e in coord_list_s)
				{
					string[] c=e.Split(sp2);
					coord_list.Add(new Vector2(float.Parse(c[0]),float.Parse(c[1])));
				}
				reco.SavePattern(temp[0],coord_list);
			}
		}
		server=new TcpListener(IPAddress.Any,3500);
		server.Start();
		while(server.Pending() || wand==null){
			wand = server.AcceptTcpClient();
		}
    }

    // Update is called once per frame
    void Update()
    {  
		try{
			stream=wand.GetStream();
			if(stream.DataAvailable){
				data.Clear();
				n_bytes=stream.Read(bytes,0,bytes.Length);
				data.AppendFormat("{0}",Encoding.ASCII.GetString(bytes,0,n_bytes));
				data_copy=data.ToString();
				data_copy=data_copy.Trim();
				Debug.Log(data_copy);
				if(data_copy.Contains("abcde")){
					Debug.Log("spell has ended");
					var res=reco.Recognize(c_list);
					byte[] msg=Encoding.ASCII.GetBytes(res.Match.Name+'\n');
					spell_being_done=res.Match.Name;
					stream.Write(msg,0,msg.Length);
					Debug.Log(res.Match.Name);
					Debug.Log(res.Score);
					c_list.Clear();
				}
				else{
					try{
						string[] temp=data_copy.Split(new char[]{','});
						if(temp.Length==3){
							c_list.Add(new Vector2(float.Parse(temp[0]),float.Parse(temp[2])));
						}
					}
					catch{}	
				}
				
			}
		}
		catch{}
    }
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,300,100),data_copy);
	}
}
