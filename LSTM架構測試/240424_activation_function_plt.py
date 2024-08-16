# -*- coding: utf-8 -*-
"""
Created on Tue Jun 25 00:35:09 2019

@author: WuGroup
"""

import matplotlib.pyplot as plt
import numpy as np
from pandas import read_csv

def MA(data, window):
    ma = data[0:(len(data)-window)].copy()
    for i in range(len(data)-window):
        ma[i] = data[i:i+window].mean()
    return ma

af = ['relu', 'Lrelu', 'elu', 'softplus']

window=50


for i in range(len(af)):
    filename = 'activation_'+str(af[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    ma = MA(loss, window)
    plt.plot(ma) 
    del x, loss, ma

plt.title('Activation Function loss comparison moving average')
plt.ylabel('loss value')
plt.ylim(0,3)
plt.xlabel('epoch')
plt.legend(['ReLU', 'LeakyReLU', 'ELU', 'Softplus'], loc ='lower left')
plt.savefig('Activation Function loss comparison MA.png')
#plt.show()
plt.clf()

window=20

for i in range(len(af)):
    filename = 'activation_'+str(af[i])+'_val.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    ma = MA(loss, window)
    plt.plot(ma)
    del x, loss, ma 

plt.title('Activation Function val loss comparison moving average')
plt.ylabel('loss value')
plt.ylim(0,3)
plt.xlabel('epoch')
plt.legend(['ReLU', 'LeakyReLU', 'ELU', 'Softplus'], loc ='upper right')
plt.savefig('Activation Function val loss comparison MA.png')
#plt.show()
plt.clf()

val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(len(af)):
    filename = 'activation_'+str(af[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = 'activation_'+str(af[i])+'_loss.csv'
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
    
print(af)
print(val_last)
print(loss_last)
print(af)
print(val_last_fifty)
print(loss_last_fifty)
print(af)
print(val_min)
print(loss_val)

