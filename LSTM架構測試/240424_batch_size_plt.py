# -*- coding: utf-8 -*-
"""
Created on Mon Jun 24 15:40:34 2019

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

bs = [64, 96, 128, 192]
#bs = [32]

window = 10

legend = []
for i in range(len(bs)):
    filename = 'batch_size_'+str(bs[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    loss = MA(loss, window)
    plt.plot(loss)
    legend.append(bs[i])
    del x, loss

plt.title('Batch Size loss moving average')
plt.ylabel('loss value')
plt.ylim(0,2)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')
plt.savefig('Batch Size loss comparison MA.png')
#plt.show()
plt.clf()

window = 20

for i in range(len(bs)):
    filename = 'batch_size_'+str(bs[i])+'_val.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    loss = MA(loss, window)
    plt.plot(loss)
    #legend.append(bs[i])
    del x, loss

plt.title('Batch Size val loss moving average')
plt.ylabel('loss value')
plt.ylim(0,2)
plt.xlabel('epoch')
plt.legend(legend, loc ='upper right')

plt.savefig('Batch Size val loss comparison MA.png')
#plt.show()
plt.clf()

val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(4):
    filename = 'batch_size_'+str(bs[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = 'batch_size_'+str(bs[i])+'_loss.csv'
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

print(bs)
print(val_last)
print(loss_last)
print(bs)
print(val_last_fifty)
print(loss_last_fifty)
print(bs)
print(val_min)
print(loss_val)