# -*- coding: utf-8 -*-
"""
Created on Tue Jul 30 13:13:40 2019

@author: wu457
"""

import matplotlib.pyplot as plt
import numpy as np
from pandas import read_csv
import random as rnd
import keras
from keras.models import model_from_json
import csv
from scipy.stats import norm
import time

recordTime =250
horizon = 250
stock_first = 300
priceC_Num = 3
LSTM_name = 'LSTM2'
DNN_name = 'DNN'
value_range = [10, 20]
valueC_range = [10, 20]

def vstack(data1, data2):
    return np.vstack((data1,data2))

def Phi(x):
    # normal cdf
    a1 = 0.254829592
    a2 = -0.284496736
    a3 = 1.421413741
    a4 = -1.453152027
    a5 = 1.061405429
    p = 0.3275911
    sign=1
    if(x<0):
        sign = -1
    x = np.abs(x) / np.sqrt(2.0)
    
    t = 1.0 / (1.0 + p * x)
    y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * np.exp(-x * x)
    return 0.5 * (1.0 + sign * y)

def GaussianVariable(mean, std):
    if(std==0):
        return mean
    u1 = rnd.uniform(0,1)
    u2 = rnd.uniform(0,1)
    
    z = np.math.sqrt(-2 * np.math.log(u1))*np.math.sin(2*np.math.pi*u2)
    x = mean + std * z
    return x

def findPrice(Mean, std, remain_t, stock):
    Mean = (int)(Mean)
    stock_num = (stock-1)
    horizon_num = (remain_t-1)*stock_first
    mean_num = (Mean-mean_range[0])*horizon*stock_first
    
    price = Y[mean_num+horizon_num+stock_num,0]
    return (int)(price)

def findPriceNew(Value, Value_C, price_C, remain_t, stock):
  Value = (int)(Value)
  Value_C = (int)(Value_C)
  Price_C = (int)(price_C)
  
  if (Price_C == 10):
      id_C = 0
  elif (Price_C == 15):
      id_C = 1
  else:
      id_C = 2
  
  stock_num = (stock-1)
  horizon_num = (remain_t-1) * stock_first
  price_num = (id_C) * horizon * stock_first
  value_num = (Value-value_range[0]) * (valueC_range[1]-valueC_range[0]+1) * priceC_Num * horizon * stock_first
  valueC_num = (Value_C-valueC_range[0]) * priceC_Num * horizon * stock_first
  price = Y[value_num + valueC_num + price_num + horizon_num + stock_num, 0]
  return (int)(price)

def Mape(y_true, y_pred): 
    return np.mean(np.abs((y_true - y_pred) / y_true)) * 100

def Rmse(predictions, targets):
    cal = ((predictions - targets) ** 2)
    cal = np.array(cal)
    return np.sqrt(cal.mean())

def simulate(mean, std, price, arrival, maxC, stock):
    sold=0
    arrival_count = 0
    np.random.seed(seed)
    for i in range(maxC):
        if(stock-sold>0):
            arrive = rnd.uniform(0,1)
            if(arrive <= arrival ):
                arrival_count += 1     
                c_prive = GaussianVariable(mean, std)
                if(c_prive >= price):
                    sold += 1
        else:
            break
    remain_product = stock-sold
    reward = sold*price
    return ( arrival_count, sold, remain_product , reward)

def simulateNew(value, value_C, price_C, price, arrival, maxC, stock):
  sold = 0
  arrival_count = 0
  np.random.seed(seed)
  sold_prop = np.exp(value-price)/(np.exp(value-price)+np.exp(value_C-price_C)+1)
  for i in range(maxC):
    if(stock-sold>0):
      arrive = rnd.uniform(0,1)
      if(arrive <= arrival):
        arrival_count += 1
        buy_prop = rnd.uniform(0,1)
        if(buy_prop <= sold_prop):
          sold += 1
    else:
      break
  remain_product = stock-sold
  reward = sold*price
  return (arrival_count, sold, remain_product, reward)

modelNum = 121
#modelNum = 1
Rowname = ['value', 'value_C', 'price_C', 'remainingTime', 'product in stock', 'best price', 'best reward']
inputNum1 = 2
inputNum2 = 5
y_num = 2
max_profit = []

filename = str(0)+str(0)+'.csv'
data = read_csv(filename, names = Rowname)
data = np.array(data)

X = data[:,0:inputNum2]
Y = data[:,inputNum2:inputNum2+1]
Y_value = data[:,inputNum2+1:inputNum2+2]

#assign pc_initial
total = 0
for i in range(0,1):
    total = Y_value[(i+1)*75000-1]
    max_profit.append((float)(total))
print(max_profit)
del data

# #period -1 max_profit
# total = 0
# for i in range(3):
#     total += Y_value[(i+1)*75000-1]
# total /= 3
# max_profit.append((float)(total))
# print(max_profit)
# del data

# # original max method
# max_profit.append(Y_value.max())
# del data

for i in range(11):
  for j in range(11):
    if (i==0 and j==0):
      continue
    else:
      filename = str(i) + str(j) +'.csv'
      data = read_csv(filename, names = Rowname)
      data = np.array(data)

      X1 = data[:,0:inputNum2]
      Y1 = data[:,inputNum2:inputNum2+1]
      Y_value = data[:,inputNum2+1:inputNum2+2]
      
      #assign pc_initial
      total = 0
      for k in range(0,1):
          total = Y_value[(k+1)*75000-1]
          max_profit.append((float)(total))
      
      #period -1 max_profit
      #total = 0
      #for k in range(3):
       #   total += Y_value[(k+1)*75000-1]
      #total /= 3
      #max_profit.append((float)(total))     
      
      #original max method
      #max_profit.append(Y_value.max())
     
      X=vstack(X,X1)
      Y=vstack(Y,Y1)

      del data, X1, Y1

print(max_profit)
print(type(max_profit))
#max_profit = np.array(max_profit)
print(len(max_profit))

priceStart = 2
priceNum = 26
price = []
for i in range(priceNum):
    price.append((priceStart+i))
price = np.array(price)

seed = 7
arrival_prob = 0.7
max_customer = 20
simu_num = 6
price_C = [10, 15, 20]

# test=[]
# for i in range(121):
#     test.append(i)

# modelNum = len(test)

R = np.zeros((modelNum,simu_num))
price_simu = np.zeros((modelNum,simu_num,horizon))

def simu(parameterType, decisionType):
    global R
    global price
    for i in range(modelNum):
        for j in range(simu_num):
            R[i,j]=0
            for t in range(horizon):
                price_simu[i,j,t]=0
                #for k in range(priceC_Num):                  
            #price_simu[i,j]=0
    if(parameterType == 'LSTM'):
        model_lstm = model_from_json(open(LSTM_name+'.json').read())
        model_lstm.load_weights(LSTM_name+'.h5')
        model_lstm.summary()
        Pred_value = np.zeros((modelNum, simu_num, 249))
        Pred_valueC = np.zeros((modelNum, simu_num, 249))
        first_name = LSTM_name
    else:
        first_name = 'Value+Value_C'
    
    if(decisionType == 'DNN'):
        model_DP = model_from_json(open(DNN_name+'.json').read())
        model_DP.load_weights(DNN_name+'.h5')
        model_DP.summary()
        last_name = DNN_name
    else:
        last_name = 'DP'
    name = first_name+'_'+last_name

    folder = 'test/'+name+'/'
    
    global test
    for k in range(simu_num):
        for m in range(modelNum):
            #j = modelNum-m-1
            #horizon = Horizon
            stock = stock_first
            #real_mean = j + mean_range[0]
            real_value = (int)(m//11)+value_range[0]
            real_value_C = (int)(m%11)+valueC_range[0]
            Reward = 0         
            
            if(decisionType == 'DNN'):
                DNN_price = []
                DNN_error = []
            
            DP_price=[]
            DP_data = np.zeros((horizon, 5))
            
            epoch=0
            # ['remainingTime', 'price', 'price_C' 'customerArrive','saleVolume']
            if(parameterType == 'LSTM'):
                LSTM_data = np.zeros((horizon, 5))
                LSTM_data[0] = [horizon, 0, 0, 0, 0]
            
            startTime = time.time()
            
            for i in range(1,horizon):
                if(parameterType == 'LSTM'):
                    data = LSTM_data.reshape(1, LSTM_data.shape[0], LSTM_data.shape[1])
                    np.random.seed(seed)
                    pred = model_lstm.predict(data)
                    pred_value = pred[0,i,0]
                    pred_valueC = pred[0,i,1]
                    #pred = pred[0,i,:]
                    #pred=pred.reshape(-1,1) #probability arrangement into a column
                    #Mean=(pred.argmax() + 10) #pick largest prob and transit tp mean_value
                    #pred_mean.append(Mean)
                    
                    if (pred_value>value_range[1]):
                        pred_value = value_range[1]
                    elif(pred_value<value_range[0]):
                        pred_value = value_range[0]

                    if (pred_valueC>valueC_range[1]):
                        pred_valueC = valueC_range[1]
                    elif(pred_valueC<valueC_range[0]):
                        pred_valueC = valueC_range[0]

                    pred_value = (int)(round(pred_value,0))
                    pred_valueC = (int)(round(pred_valueC,0))
                    Pred_value[m,k,i-1] = pred_value
                    Pred_valueC[m,k,i-1] = pred_valueC
                    Value = pred_value
                    Value_C = pred_valueC
                    #Value_C = real_value_C
                    #Pred_mean[m,k,i-1] = Mean
                    del pred

                else:
                    Value = real_value
                    Value_C = real_value_C
                
                if(decisionType == 'DNN'):
                    #DP_data_system = [Mean, Std]
                    DP_data_system = [Mean]
                    DP_data_state = [(horizon-i), stock]
                    DP_data_system = np.array(DP_data_system)
                    DP_data_state = np.array(DP_data_state)
                    #DP_data_system = DP_data_system.reshape(1,2)
                    DP_data_state = DP_data_state.reshape(1,2)
                    np.random.seed(seed)
                    price_DNN = model_DP.predict([DP_data_system, DP_data_state])
                    price_DNN = (int)(round(price_DNN[0,0],0))
                    price_DP = findPrice(Mean, Std, (horizon-i+1), stock)
                    DNN_price.append(price_DNN)
                    DNN_error.append(price_DNN-price_DP)
                    arrival_count, sold, remain_product, r = simulate(real_mean, Std, price_DNN, arrival_prob, max_customer, stock)             
                else:
                    if (i==1):
                        price_C = 10
                        price_DP = findPriceNew(Value, Value_C, price_C, (horizon-i+1), stock)
                        #price_simu[m,k,2,i-1] = price_DP
                        price_simu[m,k,i-1] = price_DP
                        DP_price.append(price_DP)
                        arrival_count, sold, remain_product, r = simulateNew(real_value, real_value_C, price_C, price_DP, arrival_prob, max_customer, stock)
                        DP_data[i-1] =  [horizon-i, price_DP, price_C, arrival_count, sold]
                    # elif (i <= 5):
                    #     id = rnd.randint(0,2)
                    #     if (id == 0):
                    #         price_C = 10
                    #     elif (id == 1):
                    #         price_C = 15
                    #     else:
                    #         price_C = 20
                    #     price_DP = rnd.randint(0,26) + 2 #2-27
                    #     #price_DP = findPriceNew(Value, Value_C, price_C, (horizon-i+1), stock)
                    #     #price_simu[m,k,id,i-1] = price_DP
                    #     price_simu[m,k,i-1] = price_DP
                    #     DP_price.append(price_DP)
                    #     arrival_count, sold, remain_product, r = simulateNew(real_value, real_value_C, price_C, price_DP, arrival_prob, max_customer, stock)
                    #     DP_data[i-1] =  [horizon-i, price_DP, price_C, arrival_count, sold]
                    else:
                        id = rnd.randint(0,2)
                        if (id == 0):
                            price_C = 10
                        elif (id == 1):
                            price_C = 15
                        else:
                            price_C = 20
                        price_DP = findPriceNew(Value, Value_C, price_C, (horizon-i+1), stock)
                        #price_simu[m,k,id,i-1] = price_DP
                        price_simu[m,k,i-1] = price_DP
                        DP_price.append(price_DP)
                        arrival_count, sold, remain_product, r = simulateNew(real_value, real_value_C, price_C, price_DP, arrival_prob, max_customer, stock)
                        DP_data[i-1] =  [horizon-i, price_DP, price_C, arrival_count, sold]
           
                stock = remain_product
                
                if(parameterType == 'LSTM'):
                    if(decisionType == 'DNN'):
                        LSTM_data[i] = [horizon-i, price_DNN, price_C, arrival_count, sold]
                    else:
                        LSTM_data[i] = [horizon-i, price_DP, price_C, arrival_count, sold]
                                                               
                Reward += r
                epoch = i
                if(remain_product == 0):
                    break 
            
            R[m, k] = Reward
            maxProfit = max_profit[m]
            #price_simu[m,k] = DP_price
            #true_mean=[]
            true_value=[]
            true_valueC=[]
            reward_error = (((maxProfit - Reward)/maxProfit)*100)
            for i in range(horizon):
                #true_mean.append(real_mean)
                true_value.append(real_value)
                true_valueC.append(real_value_C)

            #true_mean = np.array(true_mean)
            true_value = np.array(true_value)
            true_valueC = np.array(true_valueC)
            
            '''
            if(parameterType == 'LSTM'):
                plt.plot(true_mean)
                #plt.plot(pred_mean)
                plt.plot(adjt_mean)
                plt.title('Mean='+str(round(Mean,2))+' Real mean='+str(real_mean)+' R error='+str(round(reward_error,2))+' T'+str(epoch))
                plt.ylabel('value')
                plt.xlabel('period')
                plt.xlim(-5,len(pred_mean))
                plt.ylim(50,160)
                plt.legend(['real', 'pred'], loc ='best')
                plt.savefig(folder+'mean/mean_'+str(real_mean)+'_'+str(k)+'.png')
                plt.clf()
            
            if(decisionType == 'DNN'):
                DNN_error = np.array(DNN_error)
                plt.plot(DNN_error)
                plt.title('Real mean='+str(real_mean)+'DNN-DP policy')
                plt.ylabel('value')
                plt.xlabel('period')
                plt.xlim(-5,len(DNN_error))
                plt.legend(['real', 'pred'], loc ='best')
                plt.savefig(folder+'error/policy_error_'+str(real_mean)+'_'+str(k)+'.png')
                plt.clf()
            '''
            endTime = time.time()
            spend = endTime-startTime
            spend = round(spend, 1)
            epoch+=1
            if(parameterType == 'LSTM'):
                if(decisionType == 'DNN'):
                    print('LSTM_DNN',str(k),str(spend),'s', 'value=',real_value, 'valueC=',real_value_C,'R error=',str(round(reward_error,2)),'%',' T',epoch)
                else:
                    print('LSTM_DP',str(k),str(spend),'s', 'value=',real_value, 'valueC=',real_value_C,'R error=',str(round(reward_error,2)),'%',' T',epoch)
            else:
                if(decisionType == 'DNN'):
                    print('knownValue_DNN',str(k),str(spend),'s', 'value=',real_value, 'valueC=',real_value_C,'R error = ',str(round(reward_error,2)),'%',' T',epoch)
                else:
                    print('knownValue_DP',str(k),str(spend),'s', 'value=',real_value, 'valueC=',real_value_C,'R error = ',str(round(reward_error,2)),'%',' T',epoch)

            del true_value, true_valueC, DP_price

            if (k < 50):
                if (parameterType == 'LSTM'):
                    with open(folder+'LSTM_salesData_'+str(real_value)+'_'+str(real_value_C)+'_'+str(k+24)+'.csv', 'w', newline='') as csvfile:    
                        writer = csv.writer(csvfile)
                        string=[]
                        for t in range(horizon):
                            string=[]
                            remain_time = LSTM_data[t,0]
                            price = LSTM_data[t,1]
                            price_C = LSTM_data[t,2]
                            arrival = LSTM_data[t,3]
                            sold = LSTM_data[t,4]
                            string.append(remain_time)
                            string.append(price)
                            string.append(price_C)
                            string.append(arrival)
                            string.append(sold)
                            writer.writerow(string)
                            del string
                    csvfile.close()
                else:
                    with open(folder+'trueValue_salesData_'+str(real_value)+'_'+str(real_value_C)+'_'+str(k)+'.csv', 'w', newline='') as csvfile:    
                        writer = csv.writer(csvfile)
                        string=[]
                        for t in range(horizon-1):
                            string=[]
                            remain_time = DP_data[t,0]
                            price = DP_data[t,1]
                            price_C = DP_data[t,2]
                            arrival = DP_data[t,3]
                            sold = DP_data[t,4]
                            string.append(remain_time)
                            string.append(price)
                            string.append(price_C)
                            string.append(arrival)
                            string.append(sold)
                            writer.writerow(string)
                            del string
                    csvfile.close()
            # if (parameterType == 'LSTM'):
            #     with open(folder+'LSTM_salesData_'+str(real_value)+'_'+str(real_value_C)+'_'+str(k)+'.csv', 'w', newline='') as csvfile:    
            #         writer = csv.writer(csvfile)
            #         string=[]
            #         for t in range(horizon):
            #             remain_time = LSTM_data[t,0]
            #             price = LSTM_data[t,1]
            #             price_C = LSTM_data[t,2]
            #             arrival = LSTM_data[t,3]
            #             sold = LSTM_data[t,4]
            #             string.append(remain_time)
            #             string.append(price)
            #             string.append(price_C)
            #             string.append(arrival)
            #             string.append(sold)
            #             writer.writerow(string)
            #         del string
            #     csvfile.close()
            # else:
            #     with open(folder+'trueValue_salesData_'+str(real_value)+'_'+str(real_value_C)+'_'+str(k)+'.csv', 'w', newline='') as csvfile:    
            #         writer = csv.writer(csvfile)
            #         string=[]
            #         for t in range(horizon-1):
            #             remain_time = DP_data[t,0]
            #             price = DP_data[t,1]
            #             price_C = DP_data[t,2]
            #             arrival = DP_data[t,3]
            #             sold = DP_data[t,4]
            #             string.append(remain_time)
            #             string.append(price)
            #             string.append(price_C)
            #             string.append(arrival)
            #             string.append(sold)
            #             writer.writerow(string)
            #         del string
            #     csvfile.close()
                
            if(parameterType == 'LSTM'):
                del LSTM_data
            if(decisionType == 'DNN'):
                del DNN_price, DNN_error
     
    with open(folder+'price_'+name+'.csv', 'w', newline='') as csvfile:    
        writer = csv.writer(csvfile)
        for i in range(modelNum):
            for j in range(simu_num):
                string=[]
                string.append((int)(i//11)+value_range[0])
                string.append((int)(i%11)+valueC_range[0])
                for t in range(horizon-1):
                    price_temp = price_simu[i,j,t]
                    string.append(price_temp)
                writer.writerow(string)
                del string
                #for k in range(priceC_Num):
                #     for t in range(horizon-1):
                #         price_temp = price_simu[i,j,t]
                #         string.append(price_temp)
                #     writer.writerow(string)
                # del string
    csvfile.close()
    
    with open(folder+'result_'+name+'.csv', 'w', newline='') as csvfile:    
        writer = csv.writer(csvfile)
        for i in range(modelNum):      
            profit = max_profit[i]
            string = []
            #string.append((i+mean_range[0]))
            string.append((int)(i//11)+value_range[0])
            string.append((int)(i%11)+valueC_range[0])
            string.append(profit)
            for j in range(simu_num):
                string.append(R[i,j])
            writer.writerow(string)
            del string    
        writer.writerow('')
        writer.writerow(['Test Value', 'Test ValueC', 'DP Best reward', 'Test reward Mean', 'Test reward Std', 'Gap'])
        for i in range(modelNum):
            #profit = max_profit[i]
            profit = max_profit[i]
            string = []
            #string.append((test[i]))
            string.append((int)(i//11)+value_range[0])
            string.append((int)(i%11)+valueC_range[0])
            string.append(profit)
            string.append(round(R[i,:].mean(),2))
            string.append(round(R[i,:].std(),2))
            gap = ((R[i,:].mean()-profit)/profit)*100
            string.append(round(gap,4))
            writer.writerow(string)
    csvfile.close()
    
    if(parameterType == 'LSTM'):
        with open(folder+'pred.csv', 'w', newline='') as csvfile:   
            writer = csv.writer(csvfile)
            string = []
            for i in range(horizon):
                string.append(i+1)
            writer.writerow(string)
            del string
            for i in range(modelNum):
                true_value = []
                true_valueC = []
                for j in range((horizon-1)):
                    #true_mean.append((test[i]))
                    true_value.append((int)(i//11)+value_range[0])
                    true_valueC.append((int)(i%11)+valueC_range[0])
                for j in range(simu_num):
                    string = []
                    #last_mean=0
                    last_value=0
                    last_valueC=0
                    for k in range((horizon-1)):
                        if(Pred_value[i, j, k]==0 and Pred_value[i, j, k-1]!=0):
                            last_value = Pred_value[i, j, k-1]
                            string.append(last_value)
                        elif(Pred_value[i, j, k]==0):
                            string.append(last_value)
                        else:
                            string.append(Pred_value[i, j, k])
                    writer.writerow(string)
                    del string

                    string = []
                    for k in range((horizon-1)):
                        if(Pred_valueC[i, j, k]==0 and Pred_valueC[i, j, k-1]!=0):
                            last_valueC = Pred_valueC[i, j, k-1]
                            string.append(last_valueC)
                        elif(Pred_valueC[i, j, k]==0):
                            string.append(last_valueC)
                        else:
                            string.append(Pred_valueC[i, j, k])
                    writer.writerow(string)
                    del string

                    writer.writerow(true_value)
                    writer.writerow(true_valueC)
                del true_value, true_valueC
        csvfile.close()

    if(parameterType == 'LSTM'):
        del model_lstm
    if(decisionType == 'DNN'):
        del model_DP

    print('End')
simu('LSTM','DP')
simu('DP','DP')
#simu('LSTM','DNN')
#simu('DP','DP')
#simu('DP','DNN')
del test