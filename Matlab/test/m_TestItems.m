% @Author: Mihai Gurei <mihaig>
% @Date:   "Saturday, 30th December 2017, 20:40:27"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_TestItems.m
% @License: lgpl3

% Clering memory
clear all, close all, clc

% Adding up all folders to the working path
% cd ~/Documents/workstation/sapiens/Matlab
cd Matlab/
master

%% Load items
% weapon set
weapons_list = readtable('weapon1.csv');
% armor set
armor_list = readtable('armor1.csv');


%% Computing weapon DAMAGE/armor DEFENSE for all cases in the table
%damage = zeros(size(weapons_list,1), size(armor_list,1))

for weap_idx = 1:size(weapons_list,1)
    %Getting values of the weapon damages (cut, blunt and pierce)
    pure_damage = [weapons_list.CutDamage(weap_idx); weapons_list.BluntDamage(weap_idx);
        weapons_list.PierceDamage(weap_idx)];

    % Create plot
    figure(weap_idx), hold on;
    set(gca,'LineWidth', 2, 'FontSize', 10);

    for arm_idx = 1:size(armor_list,1)
        %Getting values of the armor defense (cut, blunt and pierce)
        armor = [armor_list.CutDefense(arm_idx); armor_list.BluntDefense(arm_idx);
            armor_list.PierceDefense(arm_idx);];

            % Computing the damage after armor removal
            damage(weap_idx, arm_idx) = m_Damage(pure_damage, armor);

            % Plotting
            plot(arm_idx, damage(weap_idx, arm_idx), '*', 'LineWidth', 4);
            % preparing legend
            tick_info(arm_idx) = string(armor_list.Name(arm_idx));
    end
    % Completing
    plot(1:size(armor_list,1), damage(weap_idx, 1:size(armor_list,1)), 'LineWidth', 2);
    xticklabels(tick_info)
    xtickangle(45)
    ylabel('Damage')
    title(weapons_list.Name{weap_idx})
    hold off
end

%% Plot 2D combinato
plot(damage', 'LineWidth', 2);
legend(weapons_list.Name)
xticklabels(tick_info)
xtickangle(45)
ylabel('Damage')
title('Comparison')


%% Plot 3D
figure()
surf(1:size(weapons_list,1), 1:size(armor_list,1), damage')
xticklabels(weapons_list.Name)
yticklabels(armor_list.Name)








%EOF
