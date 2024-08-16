# -*- coding: utf-8 -*-
"""
Created on Fri May  3 17:44:07 2019

@author: WuGroup
"""

import matplotlib.pyplot as plt
import numpy as np
from pandas import read_csv
#import talib

folder = 'test/'

#with dropout
for i in range(4):
    filename = 'dense_' + str(i)+'_loss.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = train[0:500]
    plt.plot(train)

plt.title('Dense layer loss comparison')
plt.ylabel('loss value')
plt.ylim(0, 1.5)
plt.xlabel('period')
plt.legend(['0 layer', '1 layer', '2 layer', '3 layer'], loc ='upper right')
plt.savefig('Dense layer number loss comparison.png')
plt.clf()

for i in range(4):
    filename = 'dense_' + str(i)+'_val.csv'
    x = read_csv(filename) 
    train = np.array(x)
    train = train[0:500]
    plt.plot(train)

plt.title('Dense layer val loss comparison')
plt.ylabel('loss value')
plt.ylim(0,1.5)
plt.xlabel('period')
plt.legend(['0 layer', '1 layer', '2 layer', '3 layer'], loc ='upper right')
plt.savefig('Dense layer number val loss comparison.png')
plt.clf()

# layer_num=[0,1,2,3]
# val_min = np.zeros((4))
# #epoch = np.zeros((4))
# loss_val = np.zeros((4))
# for i in range(4):
#     filename = 'dense_' + str(layer_num[i])+'_val.csv'
#     x = read_csv(filename) 
#     val = np.array(x)
#     filename = 'dense_' + str(layer_num[i])+'_loss.csv'
#     x = read_csv(filename) 
#     loss = np.array(x)
#     val_min[i] = val.min()
#     for j in range(1000):
#         if(val[j]==val_min[i]):
#                 loss_val[i]=loss[j]
#     del x, val, loss

# print(layer_num)
# print(val_min)
# print(loss_val)

layer_num=[0,1,2,3]
val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(4):
    filename = 'dense_' + str(i)+'_val.csv'
    x = read_csv(folder+filename) 
    val = np.array(x)
    filename = 'dense_' + str(i)+'_loss.csv'
    x = read_csv(folder+filename) 
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

print(layer_num)
print(val_last)
print(loss_last)
print(layer_num)
print(val_last_fifty)
print(loss_last_fifty)
print(layer_num)
print(val_min)
print(loss_val)




#without dropout
for i in range(4):
    filename = 'dense_' + str(i)+'_nd_loss.csv'
    x = read_csv(folder+filename) 
    train = np.array(x)
    train = train[0:500]
    plt.plot(train)

plt.title('Dense layer loss comparison_nd')
plt.ylabel('loss value')
plt.ylim(3.7, 3.9)
plt.xlabel('period')
plt.legend(['0 layer', '1 layer', '2 layer', '3 layer'], loc ='upper right')
plt.savefig(folder+'Dense layer number loss comparison_nd.png')
plt.clf()

for i in range(4):
    filename = 'dense_' + str(i)+'_nd_val.csv'
    x = read_csv(folder+filename) 
    train = np.array(x)
    train = train[0:500]
    plt.plot(train)

plt.title('Dense layer val loss comparison_nd')
plt.ylabel('loss value')
plt.ylim(3.7, 3.9)
plt.xlabel('period')
plt.legend(['0 layer', '1 layer', '2 layer', '3 layer'], loc ='upper right')
plt.savefig(folder+'Dense layer number val loss comparison_nd.png')
plt.clf()

layer_num=[0,1,2,3]
val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
for i in range(4):
    filename = 'dense_' + str(layer_num[i])+'_nd_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = 'dense_' + str(layer_num[i])+'_nd_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    val_min[i] = val.min()
    for j in range(1000):
        if(val[j]==val_min[i]):
                loss_val[i]=loss[j]
    del x, val, loss

print(layer_num)
print(val_min)
print(loss_val)

