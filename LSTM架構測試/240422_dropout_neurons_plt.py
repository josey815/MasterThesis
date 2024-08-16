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

d_rate = [0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8]
neurons = [50,100,200,300,400,500,600]
#neurons = [50]

loss_last = np.zeros((len(d_rate),len(neurons)))
loss_last_fifty = np.zeros((len(d_rate),len(neurons)))
#loss_min = np.zeros((len(d_rate),len(neurons)))
for i in range(len(d_rate)):
    for j in range(len(neurons)):
        filename = 'dropout_rate_'+str(d_rate[i])+'_neurons_'+str(neurons[j])+'_loss.csv'
        loss = read_csv(filename) 
        loss = np.array(loss)
        value = loss[-50:].mean()
        loss_last_fifty[i,j] = round(value,4)
        # value_num = loss[-1].mean()
        # loss_last[i,j] = round(value_num,4)
        del loss, value
        
val_last = np.zeros((len(d_rate),len(neurons)))
val_last_fifty = np.zeros((len(d_rate),len(neurons)))
for i in range(len(d_rate)):
    for j in range(len(neurons)):
        filename = 'dropout_rate_'+str(d_rate[i])+'_neurons_'+str(neurons[j])+'_val.csv'
        val = read_csv(filename) 
        val = np.array(val)   
        value = val[-50:].mean()
        val_last_fifty[i,j] = round(value,4)
        # value_num = val[-1].mean()
        # val_last[i,j] = round(value_num,4)
        del val, value

print(neurons)
#print(val_last)
#print(loss_last)
print(val_last_fifty)
print(loss_last_fifty)

def plot_confusion_matrix(cm,
                          title=None,
                          cmap=plt.cm.Blues):

    print(cm)

    #fig, ax = plt.subplots()
    fig, ax = plt.subplots(figsize=(10, 8))
    im = ax.imshow(cm, interpolation='nearest', cmap=cmap)
    ax.figure.colorbar(im, ax=ax)
    # We want to show all ticks...
    ax.set(xticks=np.arange(cm.shape[1]),
           yticks=np.arange(cm.shape[0]),
           # ... and label them with the respective list entries
           xticklabels=neurons, yticklabels=d_rate,
           title=title,
           ylabel='Dropout rate',
           xlabel='Neuron Units')

    # Rotate the tick labels and set their alignment.
    plt.setp(ax.get_xticklabels(), rotation=45, ha="right",
             rotation_mode="anchor")

    # Loop over data dimensions and create text annotations.
    fmt = '.4f'
    thresh = (cm.max()+cm.min()) / 2.
    for i in range(cm.shape[0]):
        for j in range(cm.shape[1]):
            ax.text(j, i, format(cm[i, j], fmt),
                    ha="center", va="center",
                    color="white" if cm[i, j] > thresh else "black")
            if (cm[i,j]==cm.min()):
                ax.text(j, i, format(cm[i, j], fmt),
                        ha="center", va="center",
                        color="red")
    #fig.tight_layout()
    plt.savefig(title+'.png')
    return ax

plot_confusion_matrix(loss_last_fifty, title='Model loss average')
plot_confusion_matrix(val_last_fifty, title='Model val loss average')

del d_rate, neurons


d_rate = [0.1, 0.2, 0.3, 0.4]
neurons = [500]

windows = 10


for i in range(len(neurons)):
    legend = []
    for j in range(len(d_rate)):
        filename = 'dropout_rate_'+str(d_rate[j])+'_neurons_'+str(neurons[i])+'_e750_loss.csv'
        x = read_csv(filename) 
        train = np.array(x)
        train = ma(train, windows)
        plt.plot(train)
        legend.append(d_rate[j])
        del train, x
        
    plt.title('Neuron 500 loss comparison')
    plt.ylabel('loss value')
    plt.ylim(4.14, 4.25)
    plt.xlabel('epoch')
    plt.legend(legend, loc ='lower left')
    plt.savefig('Neurons '+str(neurons[i])+' loss comparison.png')
    plt.clf()
    del legend

windows = 30

for i in range(len(neurons)):
    legend = []
    for j in range(len(d_rate)):
        filename = 'dropout_rate_'+str(d_rate[j])+'_neurons_'+str(neurons[i])+'_e750_val.csv'
        x = read_csv(filename) 
        train = np.array(x)
        train = ma(train, windows)
        plt.plot(train)
        legend.append(d_rate[j])
        del train, x
        
    plt.title('Neuron 500 val loss comparison')
    plt.ylabel('loss value')
    plt.ylim(4.15, 4.25)
    plt.xlabel('epoch')
    plt.legend(legend, loc ='lower left')
    plt.savefig('Neurons '+str(neurons[i])+' val loss comparison.png')
    plt.clf()
    del legend
