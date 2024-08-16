# -*- coding: utf-8 -*-
"""
Created on Sat Jul 13 19:11:04 2019

@author: wu457
"""

import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
from pandas import read_csv
from keras.models import model_from_json
import csv
import matplotlib as mpl

tsteps = 250
recordTime = tsteps

modelNum = 220
date =str(0)+str(825)
folder = 'test/'

Rowname_x = ['remainingTime', 'price_a', 'customerArrive','saleVolume']
Rowname_y = ['delta_a']

def vstack(data1, data2):
    return np.vstack((data1,data2))

filename = 'AdamFinal'
model = model_from_json(open(filename+'.json').read())
model.load_weights(filename+'.h5')
model.summary()

priceStart = 12
priceNum = 16
price = []
for i in range(priceNum):
    price.append((priceStart+i))
price = np.array(price)
print(price)

seed=7
#seed=8

ch = []
for i in range(modelNum):
    ch.append(i)

ch=np.array(ch)
np.random.seed(seed)
np.random.shuffle(ch)
#train = ch[:-484].copy()
#test = ch[:-484].copy() #for testing our training data's quality
test = ch[-22:].copy()
del ch
test.sort()

filename = 'simulation'+date+'_'+str(test[0])+'_x.csv'
X_test = read_csv(filename, names = Rowname_x)
filename = 'simulation'+date+'_'+str(test[0])+'_y.csv'
Y_test = read_csv(filename, names = Rowname_y)

for i in range(1, len(test)):
    filename = 'simulation'+date+'_'+str(test[i])+'_x.csv'
    x = read_csv(filename, names = Rowname_x)
    filename = 'simulation'+date+'_'+str(test[i])+'_y.csv'
    y = read_csv(filename, names = Rowname_y)
    X_test=vstack(X_test,x)
    Y_test=vstack(Y_test,y)
    del x , y

X_test = np.array(X_test)
Y_test = np.array(Y_test)

X_test = X_test.reshape((int)(X_test.shape[0]/tsteps),tsteps,X_test.shape[1])
X_test = np.array(X_test)
X_test = X_test.astype(float)
print(X_test.shape)

Y_test = Y_test.reshape((int)(Y_test.shape[0]/tsteps),tsteps,Y_test.shape[1])
Y_test = np.array(Y_test)
Y_test = Y_test.astype(float)
print(Y_test.shape)

string = []
for i in reversed(range(250)):
  string.append(str(i+1))

string = np.array(string)
string = string.reshape(250,1)
print(string.shape)

for i in range(22): #242
  x_test = X_test[i,:,:].copy()
  y_test = Y_test[i,:,:].copy()

  x_test = x_test.reshape((int)(x_test.shape[0]/tsteps),tsteps,x_test.shape[1])
  y_test = y_test.reshape((int)(y_test.shape[0]/tsteps),tsteps,y_test.shape[1])

  predA=[]
  #predB=[]
  pred_deltaA=[]
  real_deltaA=[]
  #pred_deltaB=[]
  #real_deltaB=[]

  np.random.seed(seed)
  pred1 = model.predict(x_test)
  print(pred1.shape)

  for j in range(recordTime):
    predA.append(pred1[0,j,0])
    #predB.append(pred1[0,j,1])

  predA = np.array(predA)
  #predB = np.array(predB)

  pred_deltaA = predA
  pred_deltaA = pred_deltaA.reshape(250,1)
  print(pred_deltaA.shape)

  real_deltaA = y_test[0,:,0]
  real_deltaA = real_deltaA.reshape(250,1)
  print(real_deltaA.shape)

  # pred_deltaB = predB
  # pred_deltaB = pred_deltaB.reshape(250,1)
  # print(pred_deltaB.shape)

  # real_deltaB = y_test[0,:,1]
  # real_deltaB = real_deltaB.reshape(250,1)
  # print(real_deltaB.shape)

  df = pd.DataFrame({'remaining time':string[:,0], 'pred_deltaA':pred_deltaA[:,0], 'real_deltaA':real_deltaA[:,0]})
  print(df.shape)
  df.to_csv(folder+'df' + str(i) + '.csv', index=False)

Rowname_output = ['remaining time', 'pred_deltaA', 'real_deltaA']
filename = folder+ 'df' + str(0) + '.csv'
output = read_csv(filename, names = Rowname_output)
output = output.drop([0])

for i in range(1, len(test)):
    filename = folder +'df' + str(i) + '.csv'
    new_output = pd.read_csv(filename, names = Rowname_output)
    new_output = new_output.drop([0])
    output=pd.concat([output,new_output], axis=1)

output = np.array(output)
print(output.shape)

split = 10
time_length = (int)(recordTime / split)

for i in range(split):
    x = []
    y = []
    #xb = []
    #yb = []

    for j in range(22):
        x.append(output[i*time_length+24, j*3+2]) #true
        y.append(output[i*time_length+24, j*3+1]) #pred
        #xb.append(output[i*time_length+24, j*5+4]) #true
        #yb.append(output[i*time_length+24, j*5+3]) #pred

    x = np.array(x)
    x = x.astype(float)
    y = np.array(y)
    y = y.astype(float)
    # xb = np.array(xb)
    # xb = xb.astype(float)
    # yb = np.array(yb)
    # yb = yb.astype(float)

    plt.plot(x, y, 'o')

    n=1
    parameter = np.polyfit(x, y, n) # n=1为一次函数，返回函数参数
    f = np.poly1d(parameter) # 拼接方程
    plt.plot(x, f(x),"r--")
    plt.plot(x, x,"k--")

    plt.title('Period '+str(i*time_length+25))
    plt.ylabel('pred valuation')
    plt.xlabel('true valuation')
    plt.xlim(9,21)
    plt.ylim(9,21)
    plt.savefig(folder+'Period_'+str(i*time_length+25)+'_value'+'.png')
    plt.clf()

    # plt.plot(xb, yb, 'o')

    # n=1
    # parameter = np.polyfit(xb, yb, n) # n=1为一次函数，返回函数参数
    # f = np.poly1d(parameter) # 拼接方程
    # plt.plot(xb, f(xb),"r--")
    # plt.plot(xb, xb,"k--")

    # plt.title('Period '+str(i*time_length+25))
    # plt.ylabel('pred valuation_C')
    # plt.xlabel('true valuation_C')
    # plt.xlim(9,21)
    # plt.ylim(9,21)
    # plt.savefig(folder+'Period_'+str(i*time_length+25)+'_valueC'+'.png')
    # plt.clf()

    #del parameter, f, x, y

# 1-5 period
for i in range(5):
    x = []
    y = []
    #xb = []
    #yb = []

    for j in range(22):
        x.append(output[i, j*3+2])
        y.append(output[i, j*3+1])
        #xb.append(output[i, j*5+4])
        #yb.append(output[i, j*5+3])

    x = np.array(x)
    x = x.astype(float)
    y = np.array(y)
    y = y.astype(float)
    # xb = np.array(xb)
    # xb = xb.astype(float)
    # yb = np.array(yb)
    # yb = yb.astype(float)

    plt.plot(x, y, 'o')

    n=1
    parameter = np.polyfit(x, y, n)
    f = np.poly1d(parameter)
    plt.plot(x, f(x),"r--")
    plt.plot(x, x,"k--")

    plt.title('Period '+str(i+1))
    plt.ylabel('pred valuation')
    plt.xlabel('true valuation')
    plt.xlim(9,21)
    plt.ylim(9,21)
    plt.savefig(folder+'Period_'+str(i+1)+'_value'+'.png')
    plt.clf()

    # plt.plot(xb, yb, 'o')


    # n=1
    # parameter = np.polyfit(xb, yb, n)
    # f = np.poly1d(parameter)
    # plt.plot(xb, f(xb),"r--")
    # plt.plot(xb, xb,"k--")

    # plt.title('Period '+str(i+1))
    # plt.ylabel('pred valuation_C')
    # plt.xlabel('true valuation_C')
    # plt.xlim(9,21)
    # plt.ylim(9,21)
    # plt.savefig(folder+'Period_'+str(i+1)+'_valueC'+'.png')
    # plt.clf()

    #del parameter, f, x, y

for i in range(5):
    x = []
    y = []
    #xb = []
    #yb = []

    for j in range(22):
        x.append(output[(i+1)*5-1, j*3+2])
        y.append(output[(i+1)*5-1, j*3+1])
        #xb.append(output[(i+1)*5-1, j*5+4])
        #yb.append(output[(i+1)*5-1, j*5+3])

    x = np.array(x)
    x = x.astype(float)
    y = np.array(y)
    y = y.astype(float)
    # xb = np.array(xb)
    # xb = xb.astype(float)
    # yb = np.array(yb)
    # yb = yb.astype(float)

    plt.plot(x, y, 'o')

    n=1
    parameter = np.polyfit(x, y, n)
    f = np.poly1d(parameter)
    plt.plot(x, f(x),"r--")
    plt.plot(x, x,"k--")

    plt.title('Period '+str((i+1)*5))
    plt.ylabel('pred valuation')
    plt.xlabel('true valuation')
    plt.xlim(9,21)
    plt.ylim(9,21)
    plt.savefig(folder+'Period_'+str((i+1)*5)+'_value'+'.png')
    plt.clf()

    # plt.plot(xb, yb, 'o')

    # n=1
    # parameter = np.polyfit(xb, yb, n)
    # f = np.poly1d(parameter)
    # plt.plot(xb, f(xb),"r--")
    # plt.plot(xb, xb,"k--")

    # plt.title('Period '+str((i+1)*5))
    # plt.ylabel('pred valuation_C')
    # plt.xlabel('true valuation_C')
    # plt.xlim(9,21)
    # plt.ylim(9,21)
    # plt.savefig(folder+'Period_'+str((i+1)*5)+'_valueC'+'.png')
    # plt.clf()

    #del parameter, f, x, y







#original code
with open(folder+'LSTM_testing.csv', 'w', newline='') as csvfile:           
    writer = csv.writer(csvfile)        
    string = []
    for i in range(250):
        string.append(str(i))
    writer.writerow(string) 
    del string
    
    for i in range(142):	
        
        x_test = X_test[i,:,:].copy()
        y_test = Y_test[i,:,:].copy()
        
        x_test = x_test.reshape((int)(x_test.shape[0]/tsteps),tsteps,x_test.shape[1])
        y_test = y_test.reshape((int)(y_test.shape[0]/tsteps),tsteps,y_test.shape[1])
    
        pred=[]
        
        np.random.seed(seed)
        pred1 = model.predict(x_test)
        #y=[]
        for j in range(recordTime):
            pred.append(pred1[0,j,:])
        #y = np.array(y)      
        pred = np.array(pred)
        pred_mean = []
        real_mean=[]
        Max =0
        Mean=0
        string1 = []
        string2 = []
        for j in range(priceNum):
            if(y_test[0,0,j]>Max):
                Mean = price[j]
                Max = y_test[0,0,j]       
        for k in range(recordTime):
            Max = 0
            Max_index=0
            for j in range(priceNum):
                if(pred[k,j]>Max):
                    Max_index=price[j]
                    Max = pred[k,j]

            string1.append(str(Max_index))
            string2.append(str(Mean))
           
                
            
        writer.writerow(string1) 
        writer.writerow(string2) 
        del pred, pred1, pred_mean, real_mean, string1, string2, x_test, y_test

    
csvfile.close()   
del X_test, Y_test

filename = 'LSTM_testing'+'.csv'
testing = read_csv(folder+filename)
testing = np.array(testing)

split = 10
time_length = (int)(recordTime / split)
for i in range(split):
    x = []
    y = []
    for j in range(142):
        x.append(testing[j*2+1, i*time_length+24])
        y.append(testing[j*2, i*time_length+24])
    x = np.array(x)
    y = np.array(y)
    
    n=1
    plt.plot(x, y, 'o')
    parameter = np.polyfit(x, y, n) # n=1为一次函数，返回函数参数
    f = np.poly1d(parameter) # 拼接方程
    plt.plot(x, f(x),"r--")
    
    plt.title('Period '+str(i*time_length+25))
    plt.ylabel('pred mean')
    plt.ylim(60,150)
    plt.xlabel('true mean')
    plt.savefig(folder+'Period_'+str(i*time_length+25)+'.png')
    plt.clf()
    del parameter, f, x, y


# 1-5 period
for i in range(5):
    x = []
    y = []
    for j in range(142):
        x.append(testing[j*2+1, i])
        y.append(testing[j*2, i])
    x = np.array(x)
    y = np.array(y)

    n=1
    plt.plot(x, y, 'o')
    parameter = np.polyfit(x, y, n)
    f = np.poly1d(parameter)
    plt.plot(x, f(x),"r--")

    plt.title('Period '+str(i+1))
    plt.ylabel('pred mean')
    plt.ylim(60,150)
    plt.xlabel('true mean')
    plt.savefig(folder+'Period_'+str(i+1)+'.png')
    plt.clf()
    del parameter, f, x, y
    
for i in range(5):
    x = []
    y = []
    for j in range(142):
        x.append(testing[j*2+1, (i+1)*5-1])
        y.append(testing[j*2, (i+1)*5-1])
    x = np.array(x)
    y = np.array(y)

    n=1
    plt.plot(x, y, 'o')
    parameter = np.polyfit(x, y, n)
    f = np.poly1d(parameter)
    plt.plot(x, f(x),"r--")

    plt.title('Period '+str((i+1)*5))
    plt.ylabel('pred mean')
    plt.ylim(60,150)
    plt.xlabel('true mean')
    plt.savefig(folder+'Period_'+str((i+1)*5)+'.png')
    plt.clf()
    del parameter, f, x, y