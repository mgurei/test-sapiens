% @Author: Mihai Gurei <mihaig>
% @Date:   "Sunday, 31st December 2017, 13:59:44"
% @Email:  mihai.gurei@analphabeta.com
% @Project: Sapiens
% @Filename: m_deadTester.m
% @License: lgpl3

% Description:


function deadmen = m_deadTester(method_name, twoD, threeD)
    % Usage:
    % method: function used to calculate
    % twoD: [boolean=true] plot 2D plot comparison
    % threeD: [boolean=true] plot 3D plot comparison

    % Load items
    weapons_list = readtable('weapon1.csv');
    armor_list = readtable('armor1.csv');

    % method finding
    method = str2func(method_name);
    parameters = 0;

    for weap_idx = 1:size(weapons_list,1)
        %Getting values of the weapon damages (cut, blunt and pierce)
        pure_damage = [weapons_list.CutDamage(weap_idx); weapons_list.BluntDamage(weap_idx);
            weapons_list.PierceDamage(weap_idx)];

        for arm_idx = 1:size(armor_list,1)
            %Getting values of the armor defense (cut, blunt and pierce)
            armor = [armor_list.CutDefense(arm_idx); armor_list.BluntDefense(arm_idx);
                armor_list.PierceDefense(arm_idx);];

                % Computing the damage after armor removal
                damage(weap_idx, arm_idx) = method(pure_damage, armor, parameters);
        end
    end

    %% Plot 2D combinato
    if twoD == true
        figure()
        plot(damage', 'LineWidth', 2);
        set(gca,'LineWidth', 2, 'FontSize', 10);
        legend(weapons_list.Name)
        xticklabels(tick_info)
        xtickangle(45)
        ylabel('Damage')
        title('2D comparison')
    end

    if threeD == true
        %% Plot 3D
        figure()
        set(gca,'LineWidth', 2, 'FontSize', 10);
        surf(1:size(weapons_list,1), 1:size(armor_list,1), damage')
        xticklabels(weapons_list.Name)
        yticklabels(armor_list.Name)
        xlabel('Weapon')
        ylabel('Armor')
        zlabel('Damage')
        title('3D comparison')
    end

end
%EOF
