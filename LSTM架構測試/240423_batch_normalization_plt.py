# -*- coding: utf-8 -*-
"""
Created on Mon Jun 24 22:46:36 2019

@author: wu457
"""

import matplotlib.pyplot as plt
import numpy as np
from pandas import read_csv

def ma(data, window):
    ma_data=data[0:-window].copy()
    for i in range(len(ma_data)):
        ma_data[i] = data[i:i+window].mean()
    return ma_data

window=10
# filename = 'Dense_BN_loss.csv'
# Dense_BN = read_csv(filename)
# Dense_BN=np.array(Dense_BN)
# Dense_BN=ma(Dense_BN,window)
filename = 'With_BN_loss.csv'
with_BN = read_csv(filename)
with_BN=np.array(with_BN)
with_BN=ma(with_BN,window)
filename = 'Without_BN_loss.csv'
without_BN = read_csv(filename)
without_BN=np.array(without_BN)
without_BN=ma(without_BN,window)

plt.plot(with_BN)
plt.plot(without_BN)
# plt.plot(Dense_BN)
plt.title('Batch normalization loss comparison moving average')
plt.ylabel('loss value')
plt.xlabel('epoch')
plt.ylim(0,1)
plt.legend(['with BN', 'Without BN'], loc ='upper right')
plt.savefig('Batch Normalization model loss comparison MA')
plt.clf()
del with_BN, without_BN

val_min = np.zeros((2))


window=20
# filename = 'Dense_BN_val.csv'
# Dense_BN = read_csv(filename)
# Dense_BN=np.array(Dense_BN)
# val_min[0] = Dense_BN.min()
# Dense_BN=ma(Dense_BN,window)
filename = 'With_BN_val.csv'
with_BN = read_csv(filename)
with_BN=np.array(with_BN)
val_min[0] = with_BN.min()
with_BN=ma(with_BN,window)
filename = 'Without_BN_val.csv'
without_BN = read_csv(filename)
without_BN=np.array(without_BN)
val_min[1] = without_BN.min()
without_BN=ma(without_BN,window)


plt.plot(with_BN)
plt.plot(without_BN)
#plt.plot(Dense_BN)
plt.title('Batch normalization val loss comparison moving average')
plt.ylabel('loss value')
plt.xlabel('epoch')
plt.ylim(0,1)
plt.legend(['with BN', 'Without BN'], loc ='upper right')
plt.savefig('Batch Normalization model val loss comparison MA')
plt.clf()
del with_BN, without_BN



val_min = np.zeros((2))
#epoch = np.zeros((4))
loss_val = np.zeros((2))

filename = 'With_BN_val.csv'
with_BN_val = read_csv(filename)
with_BN_val=np.array(with_BN_val)
filename = 'With_BN_loss.csv'
with_BN_loss = read_csv(filename)
with_BN_loss=np.array(with_BN_loss)
val_min[0] = with_BN_val.min()
for i in range(1000):
    if(with_BN_val[i]==val_min[0]):
            loss_val[0]=with_BN_loss[i]

filename = 'Without_BN_val.csv'
without_BN_val = read_csv(filename)
without_BN_val=np.array(without_BN_val)
filename = 'Without_BN_loss.csv'
without_BN_loss = read_csv(filename)
without_BN_loss=np.array(without_BN_loss)
val_min[1] = without_BN_val.min()
for i in range(1000):
    if(without_BN_val[i]==val_min[1]):
            loss_val[1]=without_BN_loss[i]

print(val_min)
print(loss_val)

val_min = np.zeros((2))
#epoch = np.zeros((4))
loss_val = np.zeros((2))
val_last = np.zeros((2))
loss_last = np.zeros((2))
val_last_tf = np.zeros((2))
loss_last_tf = np.zeros((2))

filename = 'With_BN_val.csv'
val = read_csv(filename)
val=np.array(val)
filename = 'With_BN_loss.csv'
loss = read_csv(filename)
loss=np.array(loss)

val_last[0] = val[-1]
loss_last[0] = loss[-1]

value = val[-50:].mean()
val_last_tf[0] = round(value,4)
value = loss[-50:].mean()
loss_last_tf[0] = round(value,4)

val_min[0] = val.min()
for j in range(1000):
    if(val[j]==val_min[0]):
            loss_val[0]=loss[j]
del val, loss

filename = 'Without_BN_val.csv'
val = read_csv(filename)
val=np.array(val)
filename = 'Without_BN_loss.csv'
loss = read_csv(filename)
loss=np.array(loss)

val_last[1] = val[-1]
loss_last[1] = loss[-1]

value = val[-50:].mean()
val_last_tf[1] = round(value,4)
value = loss[-50:].mean()
loss_last_tf[1] = round(value,4)

val_min[1] = val.min()
for j in range(1000):
    if(val[j]==val_min[1]):
            loss_val[1]=loss[j]
del val, loss

print(val_last)
print(loss_last)
print(val_last_tf)
print(loss_last_tf)
print(val_min)
print(loss_val)