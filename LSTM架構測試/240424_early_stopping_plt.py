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

patience = [ 200, 175, 150, 125, 100, 75, 50 ]
#patience = [200, 175]


window = 10
legend=[]

loss_min=np.zeros((7))
val_min=np.zeros((7))

for i in range(len(patience)):
    filename = 'patience_'+str(patience[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    loss_min[i] = loss[-1]
    loss = MA(loss, window) 
    legend.append(patience[i])
    plt.plot(loss) 
    del x, loss

plt.title('Model loss moving average')
plt.ylabel('loss value')
plt.ylim(0, 1.5)
plt.xlabel('period')
plt.legend(legend, loc ='upper right')
plt.savefig('Patience loss comparison MA.png')
#plt.show()
plt.clf()

window = 30
for i in range(len(patience)):
    filename = 'patience_'+str(patience[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    val_min[i] = val[-1]
    val = MA(val, window) 
    plt.plot(val) 
    del x, val

plt.title('Model val loss moving average')
plt.ylabel('loss value')
plt.ylim(0, 1.5)
plt.xlabel('period')
plt.legend(legend, loc ='upper right')
plt.savefig('Patience val loss comparison MA.png')
#plt.show()
plt.clf()

val_min = np.zeros((7))
#epoch = np.zeros((4))
loss_val = np.zeros((7))
val_last = np.zeros((7))
loss_last = np.zeros((7))
val_last_fifty = np.zeros((7))
loss_last_fifty = np.zeros((7))
for i in range(7):
    filename = 'patience_'+str(patience[i])+'_val.csv'
    x = read_csv(filename) 
    val = np.array(x)
    filename = 'patience_'+str(patience[i])+'_loss.csv'
    x = read_csv(filename) 
    loss = np.array(x)
    
    val_last[i] = val[-1]
    loss_last[i] = loss[-1]
    
    value = val[-50:].mean()
    val_last_fifty[i] = round(value,4)
    value = loss[-50:].mean()
    loss_last_fifty[i] = round(value,4)
    
    val_min[i] = val.min()
    for j in range(len(val)):
        if(val[j]==val_min[i]):
                loss_val[i]=loss[j]
    del x, val, loss

print(patience)
print(val_last)
print(loss_last)
print(patience)
print(val_last_fifty)
print(loss_last_fifty)
print(patience)
print(val_min)
print(loss_val)