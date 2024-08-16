# -*- coding: utf-8 -*-
"""
Created on Fri May  3 17:44:07 2019

@author: WuGroup
"""

import matplotlib.pyplot as plt
import numpy as np
from pandas import read_csv

def ma(data, window):
    data = np.array(data)
    ma_data=data[0:-window].copy()
    for i in range(len(ma_data)):
        ma_data[i] = data[i:i+window].mean()
    return ma_data

lr = [0.005]
opt = ['SGD', 'RMSprop','Adam']
legend=[]
window=10
for i in range(3):
    filename = str(opt[i])+'_'+str(lr[0])+'_loss.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    s = str(opt[i]) +'_lr=' + str(lr[0])
    legend.append(s)
    plt.plot(train)
    del train, s, x

plt.title('opt loss comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig('opt_loss_comparison_3_MA.png')
plt.clf()

window=20
for i in range(3):
    filename = str(opt[i])+'_'+str(lr[0])+'_val.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    plt.plot(train)
    del train, x

plt.title('opt val comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig('opt_val_comparison_3_MA.png')
plt.clf()

del legend

#lr = [0.005, 0.001, 0.0005, 0.0001]
lr = [0.05, 0.01, 0.005, 0.001]
opt = 'RMSprop'
legend=[]
window=10
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_loss.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    s = 'lr '+str(lr[i])
    legend.append(s)
    plt.plot(train)
    del train, s, x

plt.title(str(opt)+' loss comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig(str(opt)+'_loss_comparison_2_MA.png')
plt.clf()

window=20
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_val.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    plt.plot(train)
    del train, x

plt.title(str(opt)+' val comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig(str(opt)+'_val_comparison_2_MA.png')
plt.clf()

del legend

val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = str(opt)+'_'+str(lr[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    
    val_last[i] = val[-1]
    loss_last[i] = loss[-1]
    
    value = val[-50:].mean()
    val_last_fifty[i] = round(value,4)
    value = loss[-50:].mean()
    loss_last_fifty[i] = round(value,4)
    
    val_min[i] = val.min()
    for j in range(1000):
        if(val[j]==val_min[i]):
                loss_val[i]=loss[j]
    del x, val, loss

print(lr)
print(val_last)
print(loss_last)
print(lr)
print(val_last_fifty)
print(loss_last_fifty)
print(lr)
print(val_min)
print(loss_val)

#lr = [0.005, 0.001, 0.0005, 0.0001]
lr = [0.05, 0.01, 0.005, 0.001]
opt = 'Adam'
legend=[]
window=10
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_loss.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    s = 'lr '+str(lr[i])
    legend.append(s)
    plt.plot(train)
    del train, s, x

plt.title(str(opt)+' loss comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig(str(opt)+'_loss_comparison_2_MA.png')
plt.clf()

window=20
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_val.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    plt.plot(train)
    del train, x

plt.title(str(opt)+' val comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig(str(opt)+'_val_comparison_2_MA.png')
plt.clf()

val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = str(opt)+'_'+str(lr[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    
    val_last[i] = val[-1]
    loss_last[i] = loss[-1]
    
    value = val[-50:].mean()
    val_last_fifty[i] = round(value,4)
    value = loss[-50:].mean()
    loss_last_fifty[i] = round(value,4)
    
    val_min[i] = val.min()
    for j in range(1000):
        if(val[j]==val_min[i]):
                loss_val[i]=loss[j]
    del x, val, loss

print(lr)
print(val_last)
print(loss_last)
print(lr)
print(val_last_fifty)
print(loss_last_fifty)
print(lr)
print(val_min)
print(loss_val)

del legend, lr

lr = [0.05, 0.01, 0.005, 0.001]
opt = 'SGD'
legend=[]
window=10
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_loss.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    s = 'lr '+str(lr[i])
    legend.append(s)
    plt.plot(train)
    del train, s, x

plt.title(str(opt)+' loss comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig(str(opt)+'_loss_comparison_MA.png')
plt.clf()

window=20
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_val.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = ma(train, window)
    plt.plot(train)
    del train, x

plt.title(str(opt)+' val comparison')
plt.ylabel('loss value')
plt.ylim(0, 10)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig(str(opt)+'_val_comparison_MA.png')
plt.clf()

val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty= np.zeros((4))
for i in range(4):
    filename = str(opt)+'_'+str(lr[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = str(opt)+'_'+str(lr[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    
    val_last[i] = val[-1]
    loss_last[i] = loss[-1]
    
    value = val[-50:].mean()
    val_last_fifty[i] = round(value,4)
    value = loss[-50:].mean()
    loss_last_fifty[i] = round(value,4)
    
    val_min[i] = val.min()
    for j in range(2000):
        if(val[j]==val_min[i]):
                loss_val[i]=loss[j]
    del x, val, loss

print(lr)
print(val_last)
print(loss_last)
print(lr)
print(val_last_fifty)
print(loss_last_fifty)
print(lr)
print(val_min)
print(loss_val)

del legend, lr

