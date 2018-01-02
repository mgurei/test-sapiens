% @Author: Mihai Gurei <mihaig>
% @Date:   "Sunday, 31st December 2017, 16:28:03"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_testTester.m
% @License: lgpl3

%% This is a tester, In the function a string with the method name to
% be used has to be passed as the first argument. 

% Clearing memory
clear all, close all, clc

% Adding up all folders to the working path
% cd ~/Documents/workstation/sapiens/Matlab
cd Matlab/
master

m_deadTester('m_DeadMenCalc', true, false);
