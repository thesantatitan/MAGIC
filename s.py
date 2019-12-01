import serial
from dollarpy import Recognizer,Template,Point
import time

s = serial.Serial('COM4',9600)


temp_list=[]

def read_data():
	temp_f=open('template_list.txt','r')
	file_data=temp_f.read()
	file_data=file_data.strip('@')
	file_data=file_data.split('@')
	for i in file_data:
		if i!='':
			i=i.strip()
			i=i.split('#')
			k=eval(i[1])
			p_l=[]
			for j in k:
				p_l.append(Point(j[0],j[1]))
			temp_list.append(Template(i[0],p_l))
	temp_f.close()




while True:
	print("What do you wanna do 1:Add template\n2:Recognize\n3:wrong_input")
	#read_data()
	k=int(input())
	if k==1:
		rec=Recognizer([])
		s.flushInput()
		temp_f=open('template_list.txt','a')
		point_list=[]
		print("Enter name:\n")
		gesture_name=str(input())
		print("Waiting for Data\n")
		a=s.read_until(bytes('e','utf-8'))
		#print(a)
		a=a.decode('utf-8')
		a=a[:-1].split('\n')
		a=a[:-1]
		for i in a:
			i=i.split(' ')
			try:
				t1=int(float(i[0]))
				t2=int(float(i[1]))
			except ValueError:
				continue
			point_list.append((t1,t2))
		print(len(point_list))
		#point_list=rec._normalize(point_list,64)
		#temp_f.write('@'+gesture_name+'#'+str(point_list)+'')
		temp_f.write('@'+gesture_name+'#')
		for i in point_list:
			temp_f.write('*'+str(i[0])+','+str(i[1])+'*')
			print(i)
		print(len(point_list))
		temp_f.close()
		time.sleep(1)
		#read_data();

		'''a=bytes('1','utf-8')
		while a!=bytes(' \n','utf-8'):
			a=s.readline()
			gest=a.decode('utf-8')
			gest=gest.strip()
			gest=gest.split(' ')
			try:
				t1=float(gest[0])
				t2=float(gest[1])
			except ValueError:
				continue
			point_list.append(Point(t1,t2))
			print(point_list[-1])

		temp_list.append(Template(gesture_name,point_list))'''
	if k==2:
		#print(temp_list)
		#print(temp_list[0].name)
		s.flushInput()
		#print(temp_list)
		rec=Recognizer(temp_list)
		print("Waiting for Data")
		#a=bytes('1','utf-8')
		point_list=[]
		a=s.read_until(bytes('e','utf-8'))
		#print(a)
		a=a.decode('utf-8')
		a=a[:-1].split('\n')
		a=a[:-1]
		#if a==[]:
		#	print(98097)
		for i in a:
			i=i.split(' ')
			try:
				t1=float(i[0])
				t2=float(i[1])
			except ValueError:
				continue
			point_list.append(Point(t1,t2))
			#print(point_list[-1])
		'''while a!=bytes(' \n','utf-8'):
			a=s.readline()
			gest=a.decode('utf-8')
			gest=gest.strip()
			gest=gest.split(' ')
			try:
				t1=float(gest[0])
				t2=float(gest[1])
			except ValueError:
				continue
			point_list.append(Point(t1,t2))'''
		print(point_list)
		result=rec.recognize(point_list)
		print(result)



