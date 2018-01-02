% @Author: Mihai Gurei <mihaig>
% @Date:   "Sunday, 31st December 2017, 16:28:03"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_testTester.m
% @Last modified by:   mihaig
% @Last modified time: "Sunday, 31st December 2017, 17:26:01"
% @License: lgpl3

% Clearing memory
clear all, close all, clc

% Adding up all folders to the working path
% cd ~/Documents/workstation/sapiens/Matlab
cd Matlab/
master

m_deadTester('m_DeadMenCalc', true, false);
