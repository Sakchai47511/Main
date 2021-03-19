import sqlite3

order = {'order':[0,0,0,0,0,0,0],'count':[0,0,0,0,0,0,0],'price':[249,209,246,180,259,360,175]}

def menu():
    print('')
    print(15*'-','\nร้านหนังสือ ซี-อิ๊ว\n',15*'-','\n1.หนังสือทั้งหมด\n2.หนังสือแนะนำ!\n3.ค้นหาหนังสือจากชื่อผู้แต่ง\n4.เลือกสินค้า\n5.แสดงรายการที่เลือก\n6.ปิดโปรเแกรม')

def search():
    print('ค้นหาจากชื่อผู้แต่ง(พิมพ์ชื่อผู้แต่งให้ถูกต้อง)')
    while True:
        X = input('ชื่อผู้แต่ง : ')
        if X == 'เซนเซแป๊ะ':
            print('Money Summary สรุปเรื่องเงินให้เข้าใจง่ายใน 1 เล่ม')
        elif X == 'จักรพงษ์ เมษพันธุ์':
            print('Money Summary สรุปเรื่องเงินให้เข้าใจง่ายใน 1 เล่ม')
            print('Money 101 : เริ่มต้นนับหนึ่งสู่ชีวิตการเงินอุดมสุข')
        elif X == 'ธนพร เจียรนัยกุลวานิช':
            print(' 5 Steps เทรดหุ้น จากเริ่มต้น จนเทรดเป็น!')
        elif X == 'ธันย์ธรณ์ บุญจิรกิตติ์':
            print('ขายดีขึ้นทันที ด้วยเทคนิคง่าย ๆ บน Facebook')
        elif X == 'วินเนอร์':
            print('ขายดีขึ้นทันที ด้วยเทคนิคง่าย ๆ บน Facebook')
        elif X == 'สุภกฤษ กุลชาติวิจิตร':
            print('รู้แค่นี้ขายดีทุกอย่าง')
        elif X == 'โค้ชแบงค์':
            print('รู้แค่นี้ขายดีทุกอย่าง')
        elif X == 'จักรวาล นัมคณิสรณ์':
            print('จากหนุ่มโรงงานสู่เทรดเดอร์มืออาชีพ')
        elif X == 'ธนัฐ ศิริวรางกูร':
            print('กองทุนรวม 101')
        elif X == 'หมอนัท':
            print('กองทุนรวม 101')
        elif X == 'คลีนิคกองทุน':
            print('กองทุนรวม 101')
        else:
            print('"ไม่พบผลการค้นหา"')
            break

def preview():
    with sqlite3.connect('D:/Sakchairach_Python/pro1.sqlite')as con:
        sql_cmd='select * from Book'
        for row in con.execute(sql_cmd):
            print(row)

def reccommend():


def pick():
    i=0
    while(True):
        A =input('เลือกสินค้าที่ต้องการ กด 0 เพื่อออกจากโปรแกรม : ')
        if A == '1':
            order['order'][0] = 'Money Summary สรุปเรื่องเงินให้เข้าใจง่ายใน 1 เล่ม'
        elif A == '2':
            order['order'][1] = '5 Steps เทรดหุ้น จากเริ่มต้น จนเทรดเป็น!'
        elif A == '3':
            order['order'][2] = 'ขายดีขึ้นทันที ด้วยเทคนิคง่าย ๆ บน Facebook'
        elif A == '4':
            order['order'][3] = 'Money 101 : เริ่มต้นนับหนึ่งสู่ชีวิตการเงินอุดมสุข'
        elif A == '5':
            order['order'][4] = 'รู้แค่นี้ขายดีทุกอย่าง'
        elif A == '6':
            order['order'][5] = 'จากหนุ่มโรงงานสู่เทรดเดอร์มืออาชีพ'    
        elif A == '7':
            order['order'][6] = 'กองทุนรวม 101'
        else:
            break
        B =int(input('จำนวน : '))
        order['count'][i] += B
        i += 1

def preorder():
    X=0
    Z=0
    print('*'*38)
    print('-'*10 +'รายการสั่งซื้อสินค้ามีดังนี้'+'-'*10)
    print('*'*38)
    print('{0: <10}{1: <10}{2: <10}'.format('รายการ','จำนวน','ราคา'))
    for i in range(7):
        X=X+order['count'][i]
        Z=Z+(order['count'][i]*order['price'][i])
        if order['count'][i] > 0:
            print(order['order'][i],5*' ',order['count'][i],5*' ',order['price'][i])
    print('รวมทั้งหมด : ',X,5*' ',Z)


while True:
    menu()
    C =int(input('เลือกทำรายการที่ : '))
    if C == 1 :
        preview()
    elif C == 2 :
        reccommend()
    elif C == 3 :
        search()
    elif C == 4 :
        preview()
        pick()
    elif C == 5 :
        preorder()
    else:
        D =input('ต้องการออกจากโปรแกรม? (Y/N) : ')
        if D =='Y':
            break
        else:
            print('End')
