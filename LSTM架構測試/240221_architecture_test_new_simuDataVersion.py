# -*- coding: utf-8 -*-
"""
Created on Thu Aug  1 23:01:49 2019

@author: WU
"""

import matplotlib.pyplot as plt
import numpy as np
from pandas import read_csv
import tensorflow as tf
from keras.layers import Dense, Dropout, LSTM, BatchNormalization, TimeDistributed, Activation
from keras.losses import  categorical_crossentropy, KLD
from keras.metrics import categorical_accuracy
from keras.layers import LeakyReLU
from keras.activations import softmax, relu, elu,softplus
from keras.optimizers import RMSprop, Adam, SGD
from keras.callbacks import EarlyStopping
from keras.models import model_from_json
import csv
from keras.models import Sequential
#plt.switch_backend('agg')

# since we are using stateless rnn tsteps must be set to horizon size
tsteps = 250
recordTime = tsteps

modelNum = 3630
date =str(0)+str(825)
folder = 'test/'

Rowname_x = ['remainingTime', 'price_a', 'price_b', 'customerArrive','saleVolume']
Rowname_y = ['delta_a', 'delta_b']

def MA(data, window):
    data = np.array(data)
    ma = data[0:(len(data)-window)].copy()
    for i in range(len(data)-window):
        ma[i] = data[i:i+window].mean()
    return ma

def vstack(data1, data2):
    return np.vstack((data1,data2))

def Mape(y_true, y_pred):
    return np.mean(np.abs((y_true - y_pred) / y_true)) * 100

def Rmse(predictions, targets):
    return np.sqrt(((predictions - targets) ** 2).mean())

#def Weighted_mse(y_true, y_pred):
    #y_weights = np.array([2.0, 1.0])  #need to revised
    #y_weights = tf.constant([2.0, 1.0])  #need to revised
    # calculate the squared difference
    #diff = (y_true - y_pred) ** 2
    # compute the weighted mean square error
    #weighted_mean_sq_error = np.sum(diff * y_weights) / np.sum(y_weights)
    #return weighted_mean_sq_error

def weighted_mse(y_true, y_pred, weights):
    squared_errors = tf.square(y_true - y_pred)
    weighted_errors = squared_errors * weights
    return tf.reduce_mean(weighted_errors)

seed=7

ch = []
for i in range(modelNum):
    ch.append(i+1) #1-3630

ch=np.array(ch)
np.random.seed(seed)
np.random.shuffle(ch)
train = ch[:-726].copy()
#test = ch[-22:].copy()
val = ch[-726:-363].copy()
del ch
#test.sort()
val.sort()
train.sort()

# for i in range(121):
#     for j in range(30):
#         valueList =[]
#         value = (int)(i//11)+10
#         valueC = (int)(i%11)+10
#         valueList.append(value)
#         valueList.append(valueC)
#         num = i*30+j+1
#         filename = 'simulation'+date+'_y_'+str(num)+'.csv'
#         with open(folder+filename, 'w', newline='') as csvfile:
#             writer = csv.writer(csvfile)
#             #writer.writerow([ 'val acc' ])
#             for k in range(recordTime):
#                 writer.writerow(valueList)
#         csvfile.close()

filename = 'simulation'+date+'_x ('+str(val[0])+').csv'
X_val = read_csv(filename, names = Rowname_x)
filename = 'simulation'+date+'_y_'+str(val[0])+'.csv'
Y_val = read_csv(filename, names = Rowname_y)

filename = 'simulation'+date+'_x ('+str(train[0])+').csv'
X_train = read_csv(filename, names = Rowname_x)
filename = 'simulation'+date+'_y_'+str(train[0])+'.csv'
Y_train = read_csv(filename, names = Rowname_y)

for i in range(1, len(val)):
    filename = 'simulation'+date+'_x ('+str(val[i])+').csv'
    x = read_csv(filename, names = Rowname_x)
    filename = 'simulation'+date+'_y_'+str(val[i])+'.csv'
    y = read_csv(filename, names = Rowname_y)
    X_val=vstack(X_val,x)
    Y_val=vstack(Y_val,y)
    del x , y

for i in range(1, len(train)):
    filename = 'simulation'+date+'_x ('+str(train[i])+').csv'
    x = read_csv(filename, names = Rowname_x)
    filename = 'simulation'+date+'_y_'+str(train[i])+'.csv'
    y = read_csv(filename, names = Rowname_y)
    X_train=vstack(X_train,x)
    Y_train=vstack(Y_train,y)
    del x , y


#X_test = np.array(X_test)
#Y_test = np.array(Y_test)
X_val = np.array(X_val)
Y_val = np.array(Y_val)
X_train = np.array(X_train)
Y_train = np.array(Y_train)

X_val = X_val.reshape((int)(X_val.shape[0]/tsteps),tsteps,X_val.shape[1])
X_val = np.array(X_val)
X_val = X_val.astype(float)
print(X_val.shape)

Y_val = Y_val.reshape((int)(Y_val.shape[0]/tsteps),tsteps,Y_val.shape[1])
Y_val = np.array(Y_val)
Y_val = Y_val.astype(float)
print(Y_val.shape)

X_train = X_train.reshape((int)(X_train.shape[0]/tsteps),tsteps,X_train.shape[1])
X_train = np.array(X_train)
X_train = X_train.astype(float)
print(X_train.shape)

Y_train = Y_train.reshape((int)(Y_train.shape[0]/tsteps),tsteps,Y_train.shape[1])
Y_train = np.array(Y_train)
Y_train = Y_train.astype(float)
print(Y_train.shape)

del train, val

print('Creating Model')

def testModel(name, opt='Adam', learning_rate=0.005, lstm_layers=1, if_dropout=True, dense_layers=0,
              dropout_rate=0.1, neurons=100,activation='relu', batch_normalization='Without',
              batch_size=96, if_early_stopping=True, myPatience=150, epoch=2000):


    np.random.seed(seed)
    model = Sequential()
    model.add(LSTM(neurons,return_sequences=True,input_shape=( tsteps, X_train.shape[2])))
    if(if_dropout):
        model.add(Dropout(dropout_rate))
    if(batch_normalization == 'With'):
        model.add(BatchNormalization())

    for i in range(lstm_layers-1):
        model.add(LSTM(neurons, return_sequences=True))
        if(if_dropout):
            model.add(Dropout(dropout_rate))
        if(batch_normalization == 'With'):
            model.add(BatchNormalization())
        if(activation == 'relu'):
            model.add(Activation('relu'))
        elif(activation == 'Lrelu'):
            model.add(LeakyReLU(0.3))
        elif(activation == 'elu'):
            model.add(Activation('elu'))
        else:
            model.add(Activation('softplus'))
    for i in range(dense_layers):
        model.add(TimeDistributed(Dense(neurons)))
        if(batch_normalization != 'Without'):
            model.add(BatchNormalization())
        if(activation == 'relu'):
            model.add(Activation('relu'))
        elif(activation == 'Lrelu'):
            model.add(LeakyReLU(0.3))
        elif(activation == 'elu'):
            model.add(Activation('elu'))
        else:
            model.add(Activation('softplus'))
        model.add(Dropout(dropout_rate))

    model.add(TimeDistributed(Dense(Y_train.shape[2])))
    #if(batch_normalization != 'Without'):
     #   model.add(BatchNormalization())
    #model.add(Activation('relu'))

    #sample_weight = tf.constant([2, 1])
    weights = np.array([2.0, 1.0])
    if(opt == 'Adam'):
        #model.compile(loss=categorical_crossentropy, optimizer=Adam(lr = learning_rate), metrics = ['categorical_accuracy'])
        #model.compile(loss="mean_absolute_error", optimizer=Adam(lr = learning_rate), metrics = ['mean_absolute_error'])
        model.compile(loss="mean_squared_error", optimizer=Adam(lr = learning_rate), metrics = ['mean_squared_error'])
        #model.compile(optimizer=Adam(lr = learning_rate), loss=lambda y_true, y_pred: weighted_mse(y_true, y_pred, weights), metrics=[lambda y_true, y_pred: weighted_mse(y_true, y_pred, weights)])
    elif(opt == 'RMSprop'):
        #model.compile(loss=categorical_crossentropy, optimizer=RMSprop(lr = learning_rate), metrics = ['categorical_accuracy'])
        #model.compile(loss="mean_absolute_error", optimizer=RMSprop(lr = learning_rate), metrics = ['mean_absolute_error'])
        model.compile(loss="mean_squared_error", optimizer=RMSprop(lr = learning_rate), metrics = ['mean_squared_error'])
        #model.compile(optimizer=RMSprop(lr = learning_rate), loss=lambda y_true, y_pred: weighted_mse(y_true, y_pred, weights), metrics=[lambda y_true, y_pred: weighted_mse(y_true, y_pred, weights)])
    elif(opt == 'SGD'):
        #model.compile(loss=categorical_crossentropy, optimizer=SGD(lr = learning_rate), metrics = ['categorical_accuracy'])
        #model.compile(loss="mean_absolute_error", optimizer=SGD(lr = learning_rate), metrics = ['mean_absolute_error'])
        model.compile(loss="mean_squared_error", optimizer=SGD(lr = learning_rate), metrics = ['mean_squared_error'])
        #model.compile(optimizer=SGD(lr = learning_rate), loss=lambda y_true, y_pred: weighted_mse(y_true, y_pred, weights), metrics=[lambda y_true, y_pred: weighted_mse(y_true, y_pred, weights)])

    earlyStopping = EarlyStopping(monitor='val_loss', patience=myPatience, min_delta= 0.0001, mode='min')
    model.summary()

    print('Training')

    if(if_early_stopping):
        np.random.seed(seed)
        history = model.fit(X_train,
                            Y_train,
                            batch_size=batch_size,
                            verbose=2,
                            epochs=epoch,
                            shuffle=True,
                            validation_data=(X_val, Y_val),
                            callbacks= [earlyStopping])
    else:
        np.random.seed(seed)
        history = model.fit(X_train,
                            Y_train,
                            batch_size=batch_size,
                            verbose=2,
                            epochs=epoch,
                            shuffle=True,
                            validation_data=(X_val, Y_val))

    model_json = model.to_json()

    filename = name
    open(folder+filename+'.json', 'w').write(model_json)
    model.save_weights(folder+filename+'.h5')

    print('End of training')

    window = 20
    loss = history.history['loss']
    val = history.history['val_loss']


    loss_ma = MA(loss, window)
    val_ma = MA(val, window)

    plt.plot(loss_ma)
    plt.plot(val_ma)
    plt.title('Model Loss moving average')
    plt.ylabel('loss')
    plt.xlabel('epoch')
    plt.ylim(0, 30)
    plt.legend(['train', 'val'], loc ='upper right')
    plt.savefig(folder+filename+'_modelLoss MA.png')
    plt.clf()


    #loss_acc = history.history['mean_absolute_error']
    #val_acc = history.history['val_mean_absolute_error']
    loss_acc = history.history['loss']
    val_acc = history.history['val_loss']


    loss_acc_ma = MA(loss_acc, window)
    val_acc_ma = MA(val_acc, window)


    plt.plot(loss_acc_ma)
    plt.plot(val_acc_ma)

    plt.title('Model Accuracy moving average')
    plt.ylabel('accuracy')
    plt.xlabel('epoch')
    plt.ylim(0,15)
    plt.legend(['train', 'val'], loc ='upper right')
    plt.savefig(folder+filename+'_model accuracy MA.png')
    plt.clf()


    with open(folder+filename+'_loss.csv', 'w', newline='') as csvfile:
        writer = csv.writer(csvfile)
        writer.writerow([ 'model loss' ])
        for i in range(len(loss)):
            writer.writerow([loss[i]])
    csvfile.close()

    with open(folder+filename+'_val.csv', 'w', newline='') as csvfile:
        writer = csv.writer(csvfile)
        writer.writerow([ 'model acc' ])
        for i in range(len(val)):
            writer.writerow([val[i]])
    csvfile.close()

    with open(folder+filename+'_trainAcc.csv', 'w', newline='') as csvfile:
        writer = csv.writer(csvfile)
        writer.writerow([ 'train acc' ])
        for i in range(len(loss_acc)):
            writer.writerow([loss_acc[i]])
    csvfile.close()

    with open(folder+filename+'_valAcc.csv', 'w', newline='') as csvfile:
        writer = csv.writer(csvfile)
        writer.writerow([ 'val acc' ])
        for i in range(len(val_acc)):
            writer.writerow([val_acc[i]])
    csvfile.close()

    del loss, loss_acc, loss_acc_ma, loss_ma, val, val_acc, val_acc_ma, val_ma, model_json, model
    
# testModel(name='Adam_0.005', opt = 'Adam', learning_rate=0.005, epoch=1000)
# testModel(name='Adam_0.001', opt = 'Adam', learning_rate=0.001, epoch=1000)
# testModel(name='Adam_0.0005', opt = 'Adam', learning_rate=0.0005, epoch=1000)
# testModel(name='Adam_0.0001', opt = 'Adam', learning_rate=0.0001, epoch=1000)
#testModel(name='RMSprop_0.005', opt = 'RMSprop', learning_rate=0.005, epoch=1000)
#testModel(name='RMSprop_0.0005', opt = 'RMSprop', learning_rate=0.0005, epoch=1000)
#testModel(name='RMSprop_0.001', opt = 'RMSprop', learning_rate=0.001, epoch=1000)
#testModel(name='RMSprop_0.0001', opt = 'RMSprop', learning_rate=0.0001, epoch=1000)
#testModel(name='SGD_0.01', opt = 'SGD', learning_rate=0.01, epoch=2000)
#testModel(name='SGD_0.001', opt = 'SGD', learning_rate=0.001, epoch=2000)
#testModel(name='SGD_0.05', opt = 'SGD', learning_rate=0.05, epoch=2000)
#testModel(name='SGD_0.005', opt = 'SGD', learning_rate=0.005, epoch=2000)
#testModel(name='lstm_3',lstm_layers=3,if_dropout=True)
#testModel(name='lstm_2',lstm_layers=2,if_dropout=True)
#testModel(name='lstm_1',lstm_layers=1,if_dropout=True)
#testModel(name='lstm_3_nd',lstm_layers=3,if_dropout=False)
#testModel(name='lstm_2_nd',lstm_layers=2,if_dropout=False)
#testModel(name='lstm_1_nd',lstm_layers=1,if_dropout=False)
#testModel(name='dense_0',dense_layers=0,epoch=1000)
#testModel(name='dense_1',dense_layers=1,epoch=1000)
#testModel(name='dense_2',dense_layers=2,epoch=1000)
#testModel(name='dense_3',dense_layers=3,epoch=1000)
'''
testModel(name='dropout_rate_0.1_neurons_100',dropout_rate=0.1,neurons=100)
testModel(name='dropout_rate_0.2_neurons_100',dropout_rate=0.2,neurons=100)
testModel(name='dropout_rate_0.3_neurons_100',dropout_rate=0.3,neurons=100)
testModel(name='dropout_rate_0.4_neurons_100',dropout_rate=0.4,neurons=100)
testModel(name='dropout_rate_0.5_neurons_100',dropout_rate=0.5,neurons=100)
testModel(name='dropout_rate_0.6_neurons_100',dropout_rate=0.6,neurons=100)
testModel(name='dropout_rate_0.7_neurons_100',dropout_rate=0.7,neurons=100)
testModel(name='dropout_rate_0.8_neurons_100',dropout_rate=0.8,neurons=100)
testModel(name='dropout_rate_0.1_neurons_200',dropout_rate=0.1,neurons=200)
testModel(name='dropout_rate_0.2_neurons_200',dropout_rate=0.2,neurons=200)
testModel(name='dropout_rate_0.3_neurons_200',dropout_rate=0.3,neurons=200)
testModel(name='dropout_rate_0.4_neurons_200',dropout_rate=0.4,neurons=200)
testModel(name='dropout_rate_0.5_neurons_200',dropout_rate=0.5,neurons=200)
testModel(name='dropout_rate_0.6_neurons_200',dropout_rate=0.6,neurons=200)
testModel(name='dropout_rate_0.7_neurons_200',dropout_rate=0.7,neurons=200)
testModel(name='dropout_rate_0.8_neurons_200',dropout_rate=0.8,neurons=200)
testModel(name='dropout_rate_0.1_neurons_300',dropout_rate=0.1,neurons=300)
testModel(name='dropout_rate_0.2_neurons_300',dropout_rate=0.2,neurons=300)
testModel(name='dropout_rate_0.3_neurons_300',dropout_rate=0.3,neurons=300)
testModel(name='dropout_rate_0.4_neurons_300',dropout_rate=0.4,neurons=300)
testModel(name='dropout_rate_0.5_neurons_300',dropout_rate=0.5,neurons=300)
testModel(name='dropout_rate_0.6_neurons_300',dropout_rate=0.6,neurons=300)
testModel(name='dropout_rate_0.7_neurons_300',dropout_rate=0.7,neurons=300)
testModel(name='dropout_rate_0.8_neurons_300',dropout_rate=0.8,neurons=300)
testModel(name='dropout_rate_0.1_neurons_400',dropout_rate=0.1,neurons=400)
testModel(name='dropout_rate_0.2_neurons_400',dropout_rate=0.2,neurons=400)
testModel(name='dropout_rate_0.3_neurons_400',dropout_rate=0.3,neurons=400)

testModel(name='dropout_rate_0.4_neurons_400',dropout_rate=0.4,neurons=400)
testModel(name='dropout_rate_0.5_neurons_400',dropout_rate=0.5,neurons=400)
testModel(name='dropout_rate_0.6_neurons_400',dropout_rate=0.6,neurons=400)
testModel(name='dropout_rate_0.7_neurons_400',dropout_rate=0.7,neurons=400)
testModel(name='dropout_rate_0.8_neurons_400',dropout_rate=0.8,neurons=400)
testModel(name='dropout_rate_0.1_neurons_500',dropout_rate=0.1,neurons=500)
testModel(name='dropout_rate_0.2_neurons_500',dropout_rate=0.2,neurons=500)
testModel(name='dropout_rate_0.3_neurons_500',dropout_rate=0.3,neurons=500)
testModel(name='dropout_rate_0.4_neurons_500',dropout_rate=0.4,neurons=500)
testModel(name='dropout_rate_0.5_neurons_500',dropout_rate=0.5,neurons=500)
testModel(name='dropout_rate_0.6_neurons_500',dropout_rate=0.6,neurons=500)
testModel(name='dropout_rate_0.7_neurons_500',dropout_rate=0.7,neurons=500)
testModel(name='dropout_rate_0.8_neurons_500',dropout_rate=0.8,neurons=500)

testModel(name='dropout_rate_0.1_neurons_600',dropout_rate=0.1,neurons=600)
testModel(name='dropout_rate_0.2_neurons_600',dropout_rate=0.2,neurons=600)
testModel(name='dropout_rate_0.3_neurons_600',dropout_rate=0.3,neurons=600)
testModel(name='dropout_rate_0.4_neurons_600',dropout_rate=0.4,neurons=600)

testModel(name='dropout_rate_0.5_neurons_600',dropout_rate=0.5,neurons=600)
testModel(name='dropout_rate_0.6_neurons_600',dropout_rate=0.6,neurons=600)
testModel(name='dropout_rate_0.7_neurons_600',dropout_rate=0.7,neurons=600)
testModel(name='dropout_rate_0.8_neurons_600',dropout_rate=0.8,neurons=600)
testModel(name='Dense_BN',batch_normalization='Dense')
testModel(name='With_BN',batch_normalization='With')
testModel(name='Without_BN',batch_normalization='Without')


testModel(name='dropout_rate_0.1_neurons_500_e750',dropout_rate=0.1,neurons=500, epoch=750)
testModel(name='dropout_rate_0.2_neurons_500_e750',dropout_rate=0.2,neurons=500, epoch=750)
testModel(name='dropout_rate_0.3_neurons_500_e750',dropout_rate=0.3,neurons=500, epoch=750)
testModel(name='dropout_rate_0.4_neurons_500_e750',dropout_rate=0.4,neurons=500, epoch=750)

#testModel(name='Dense_BN',batch_normalization='Dense',epoch=1000)
testModel(name='With_BN',batch_normalization='With',epoch=1000)
testModel(name='Without_BN',batch_normalization='Without',epoch=1000)
testModel(name='activation_relu', activation='relu',epoch=1000)
testModel(name='activation_elu', activation='elu',epoch=1000)
testModel(name='activation_Lrelu', activation='Lrelu',epoch=1000)
testModel(name='activation_softplus', activation='softplus',epoch=1000)
testModel(name='batch_size_64', batch_size=64,epoch=1000)
testModel(name='batch_size_96', batch_size=96,epoch=1000)
testModel(name='batch_size_128', batch_size=128,epoch=1000)
testModel(name='batch_size_192', batch_size=192,epoch=1000)
'''

# testModel(name='patience_50',if_early_stopping=True,myPatience=50, epoch=2000)
# testModel(name='patience_75',if_early_stopping=True,myPatience=75, epoch=2000)
# testModel(name='patience_100',if_early_stopping=True,myPatience=100, epoch=2000)
# testModel(name='patience_125',if_early_stopping=True,myPatience=125, epoch=2000)
# testModel(name='patience_150',if_early_stopping=True,myPatience=150, epoch=2000)

testModel(name='AdamFinal', opt = 'Adam', if_early_stopping=True, learning_rate=0.005, myPatience=150, epoch=2000)
#testModel(name='Adam_final',if_early_stopping=True,myPatience=100, epoch=2000)
# testModel(name='RMSprop_final', opt = 'RMSprop', learning_rate=0.0005,if_early_stopping=True,myPatience=100, epoch=2000)
