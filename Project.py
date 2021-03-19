#import sqlite เข้ามา
import sqlite3
#สร้างSetขึ้นมา
#กำหนดListเข้าไปในSet
order = {'order':[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],'count':[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],'price':[494,1250,280,763,458,488,702,855,495,458,366,764,468,733,921]}
#Func หน้าเมนู
def menu():
    print('')
    print(25*'-','\n   SE EW BOOK SELLERS')
    print(25*'-','\n1.All Books!\n2.Best Sellers!\n3.About The Books!\n4.Put in Bag!\n5.Show Selection!\n6.Exits!')

#Func แสดงหนังสือทั้งหมด โดยมี SQLite เป็น DB
def preview():
    print(15*'-','\n  All Books!')
    print(15*'-')
    print('{0: <5}{1: <15}{2: <20}'.format('No.','BookName','Price')) #กำหนด Title
    print(35*'-')
    with sqlite3.connect('D:/Sakchairach_Python/Last Pro/pro1.sqlite')as con: #เรียกข้อมูลDBจากไฟล์SQLite
        sql_cmd='select * from Book' #โดยเลือกทั้งหมดจากTable(แฟ้ม)ที่สร้างไว้
        for row in con.execute(sql_cmd): #ใช้ Loop ในการรัน
            print(row[0],'.',row[1],5*'-',row[2],row[3]) #แสดงผลออกมา

#Func แสดงเกี่ยวกับบหนังสือ โดยมี SQLite เป็น DB
def about():
    print(22*'-','\n  About The Books')
    print(22*'-')
    with sqlite3.connect('D:/Sakchairach_Python/Last Pro/pro1.sqlite')as con: #เรียกข้อมูลDBจากไฟล์SQLite
        sql_cmd='select * from About' #โดยเลือกทั้งหมดจากTable(แฟ้ม)ที่สร้างไว้
        for row in con.execute(sql_cmd): #ใช้ Loop ในการรัน
            print([row[0]],'.','\n  :',row[1],'\n',75*'-')

#Func หนังสือแนะนำ
def recommend():
    print(20*'-')    
    print('  Best Sellers!')
    print(20*'-')
    print('{0: <5}{1: <15}{2: <20}'.format('No.','BookName','Price')) #กำหนด Title
    print(35*'-')    
    with sqlite3.connect('D:/Sakchairach_Python/Last Pro/pro1.sqlite')as con: #เรียกข้อมูลDBจากไฟล์SQLite
        sql_cmd='select * from BookRec' #โดยเลือกทั้งหมดจากTable(แฟ้ม)ที่สร้างไว้
        for row in con.execute(sql_cmd): #ใช้ Loop ในการรัน
            print(row[0],'.',row[1],5*'-',row[2],row[3]) #แสดงผลออกมา

#Func เพิ่มหนังสือเข้าตะกร้า
def pick():
    #ไม่รู้วิธีดึงไฟล์จาก sql มาเพิ่มใน list เลย ใช้วิธีเพิ่มมันเข้าไปตรงๆเลย \(>_<)/
    while True :
        A =input('Select the desired product and press 0 to exit the program. : ')
        order['order'][0] = '1.The Vanishing Half'
        order['order'][1] = '2.A Promised Land'
        order['order'][2] = '3.Caste: The Origins of Our Discontents'
        order['order'][3] = '4.Untamed'
        order['order'][4] = '5.Greenlights'
        order['order'][5] = '6.American Dirt'    
        order['order'][6] = '7.The Midnight Library'
        order['order'][7] = '8.The Return'
        order['order'][8] = '9.Home Body'
        order['order'][9] = '10.Shuggie Bain'
        order['order'][10] = '11.Deacon King Kong: A Novel'
        order['order'][11] = '12.The Sanatorium'
        order['order'][12] = '13.Hamnet'
        order['order'][13] = '14.The Invisible Life of Addie LaRue'
        order['order'][14] = '15.Modern Comfort Food'
        if A == '0' :
            break
        #คำนวนจำนวนที่เพิ่มเข้าไปในตะกร้า
        else:
            B =int(input('Amount : '))
            order['count'][int(A)-1] += B

#Func แสดงหนังสือที่เพิ่มเข้าตะกร้า
def preorder():
    X=0
    Z=0
    i=0
    print('*'*38)
    print('-'*10 +'Check your orders'+'-'*10)
    print('*'*38)
    print('{0: <20}{1: <20}{2}'.format('No.','Amount','Price')) #กำหนด Title
    print('*'*50)

#แสดงผลของสินค้าที่เพิ่มเข้าไปในตะกร้า
    for i in range(len(order['order'])): 
        if order['count'][i] <= len(order['count']):
            if order['count'][i] != 0 :
                print(order['order'][i],5*' ',order['count'][i],5*' ',order['price'][i],'Baht')
        X=X+order['count'][i]
        Z=Z+(order['count'][i]*order['price'][i])
    print('Total : ',X,5*' ',Z,' Baht') #แสดงผลจำนวนหนังสือ และราคารวมทั้งหมด

#ตัวรันโปรแกรม
while True:
    menu()
    C =int(input('Select menu : '))
    if C == 1 :
        preview()
    elif C == 2 :
        recommend()
    elif C == 3 :
        about()
    elif C == 4 :
        print('\n --> Select the Books <-- \n')
        preview()
        pick()
    elif C == 5 :
        preorder()
    else:
        D =input('Wanna Exits? (Y/N) : ')
        if D =='Y':
            break
        elif D =='y':
            break