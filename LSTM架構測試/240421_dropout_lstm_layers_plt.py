# -*- coding: utf-8 -*-
"""
Created on Mon Jun 24 22:46:36 2019

@author: wu457
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


# without dropout
filename = 'lstm_1_nd_loss.csv'
layer1 = read_csv(filename)

filename = 'lstm_2_nd_loss.csv'
layer2 = read_csv(filename)

filename = 'lstm_3_nd_loss.csv'
layer3 = read_csv(filename)

window=5

layer1=ma(layer1,window)
layer2=ma(layer2,window)
layer3=ma(layer3,window)

plt.plot(layer1)
plt.plot(layer2)
plt.plot(layer3)
plt.title('LSTM layers without dropout loss moving average')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.ylim(0,3)
plt.legend(['layer1', 'layer2', 'layer3'], loc ='upper right')
plt.savefig('LSTM layers without dropout loss MA')
plt.clf()

del layer1, layer2, layer3


filename = 'lstm_1_nd_val.csv'
layer1 = read_csv(filename)

filename = 'lstm_2_nd_val.csv'
layer2 = read_csv(filename)

filename = 'lstm_3_nd_val.csv'
layer3 = read_csv(filename)

window=20

layer1=ma(layer1,window)
layer2=ma(layer2,window)
layer3=ma(layer3,window)


plt.plot(layer1)
plt.plot(layer2)
plt.plot(layer3)
plt.title('LSTM layers without dropout val loss moving average')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.ylim(0,3)
plt.legend(['layer1', 'layer2', 'layer3'], loc ='best')
plt.savefig('LSTM layers without dropout val MA')
plt.clf()
del layer1, layer2, layer3

# layer_num=[1,2,3]
# val_min = np.zeros((3))
# #epoch = np.zeros((4))
# loss_val = np.zeros((3))
# for i in range(3):
#     filename = 'lstm_' + str(layer_num[i])+'_nd_val.csv'
#     x = read_csv(filename) 
#     val = np.array(x)
#     filename = 'lstm_' + str(layer_num[i])+'_nd_loss.csv'
#     x = read_csv(filename) 
#     loss = np.array(x)
#     val_min[i] = val.min()
#     for j in range(500):
#         if(val[j]==val_min[i]):
#                 loss_val[i]=loss[j]
#     del x, val, loss

# print(layer_num)
# print(val_min)
# print(loss_val)

layer_num=[1,2,3,4]
val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(4):
    filename = 'lstm_' + str(layer_num[i])+'_nd_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = 'lstm_' + str(layer_num[i])+'_nd_loss.csv'
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

print(layer_num)
print(val_last)
print(loss_last)
print(layer_num)
print(val_last_fifty)
print(loss_last_fifty)
print(layer_num)
print(val_min)
print(loss_val)


# with dropout
filename = 'lstm_1_loss.csv'
layer1 = read_csv(filename)

filename = 'lstm_2_loss.csv'
layer2 = read_csv(filename)

filename = 'lstm_3_loss.csv'
layer3 = read_csv(filename)

window=5

layer1=ma(layer1,window)
layer2=ma(layer2,window)
layer3=ma(layer3,window)

plt.plot(layer1)
plt.plot(layer2)
plt.plot(layer3)
plt.title('LSTM layers with dropout loss moving average')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.ylim(0,3)
plt.legend(['layer1', 'layer2', 'layer3'], loc ='upper right')
plt.savefig('LSTM layers with dropout loss MA')
plt.clf()

del layer1, layer2, layer3


filename = 'lstm_1_val.csv'
layer1 = read_csv(filename)

filename = 'lstm_2_val.csv'
layer2 = read_csv(filename)

filename = 'lstm_3_val.csv'
layer3 = read_csv(filename)

window=10

layer1=ma(layer1,window)
layer2=ma(layer2,window)
layer3=ma(layer3,window)


plt.plot(layer1)
plt.plot(layer2)
plt.plot(layer3)
plt.title('LSTM layers with dropout val loss moving average')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.ylim(0,3)
plt.legend(['layer1', 'layer2', 'layer3'], loc ='upper right')
plt.savefig('LSTM layers with dropout val MA')
plt.clf()

del layer1, layer2, layer3

# layer_num=[1,2,3]
# val_min = np.zeros((3))
# #epoch = np.zeros((4))
# loss_val = np.zeros((3))
# for i in range(3):
#     filename = 'lstm_' + str(layer_num[i])+'_val.csv'
#     x = read_csv(filename) 
#     val = np.array(x)
#     filename = 'lstm_' + str(layer_num[i])+'_loss.csv'
#     x = read_csv(filename) 
#     loss = np.array(x)
#     val_min[i] = val.min()
#     for j in range(500):
#         if(val[j]==val_min[i]):
#                 loss_val[i]=loss[j]
#     del x, val, loss

# print(layer_num)
# print(val_min)
# print(loss_val)

layer_num=[1,2,3,4]
val_min = np.zeros((4))
#epoch = np.zeros((4))
loss_val = np.zeros((4))
val_last = np.zeros((4))
loss_last = np.zeros((4))
val_last_fifty = np.zeros((4))
loss_last_fifty = np.zeros((4))
for i in range(4):
    filename = 'lstm_' + str(layer_num[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = 'lstm_' + str(layer_num[i])+'_loss.csv'
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

print(layer_num)
print(val_last)
print(loss_last)
print(layer_num)
print(val_last_fifty)
print(loss_last_fifty)
print(layer_num)
print(val_min)
print(loss_val)




filename = 'lstm_1_nd_loss.csv'
layer1_nd = read_csv(filename)
'''
filename = 'lstm_2_nd_loss.csv'
layer2_nd = read_csv(filename)

filename = 'lstm_3_nd_loss.csv'
layer3_nd = read_csv(filename)

filename = 'lstm_1_loss.csv'
layer1 = read_csv(filename)
'''
filename = 'lstm_2_loss.csv'
layer2 = read_csv(filename)
'''
filename = 'lstm_3_loss.csv'
layer3 = read_csv(filename)
'''
window=20

layer1_nd=ma(layer1_nd,window)
#layer2_nd=ma(layer2_nd,window)
#layer3_nd=ma(layer3_nd,window)
#layer1=ma(layer1,window)
layer2=ma(layer2,window)
#layer3=ma(layer3,window)

plt.plot(layer1_nd)
#plt.plot(layer2_nd)
#plt.plot(layer3_nd)
#plt.plot(layer1)
plt.plot(layer2)
#plt.plot(layer3)
plt.title('LSTM layers with or without dropout loss moving average')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.ylim(4.15, 4.3)
plt.legend(['Without Dropout','With Dropout'], loc ='upper right')
plt.savefig('LSTM layers with or without dropout loss MA')
plt.clf()
del layer2, layer1_nd

filename = 'lstm_1_nd_val.csv'
layer1_nd = read_csv(filename)
'''
filename = 'lstm_2_nd_val.csv'
layer2_nd = read_csv(filename)

filename = 'lstm_3_nd_val.csv'
layer3_nd = read_csv(filename)

filename = 'lstm_1_val.csv'
layer1 = read_csv(filename)
'''
filename = 'lstm_2_val.csv'
layer2 = read_csv(filename)
'''
filename = 'lstm_3_val.csv'
layer3 = read_csv(filename)
'''
window=20

layer1_nd=ma(layer1_nd,window)
#layer2_nd=ma(layer2_nd,window)
#layer3_nd=ma(layer3_nd,window)
#layer1=ma(layer1,window)
layer2=ma(layer2,window)
#layer3=ma(layer3,window)

plt.plot(layer1_nd)
#plt.plot(layer2_nd)
#plt.plot(layer3_nd)
#plt.plot(layer1)
plt.plot(layer2)
#plt.plot(layer3)
plt.title('LSTM layers with or without dropout val loss moving average')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.ylim(4.15, 4.4)
plt.legend(['Without Dropout','With Dropout'], loc ='upper right')
plt.savefig('LSTM layers with or without dropout val loss MA')
plt.clf()
del layer2, layer1_nd